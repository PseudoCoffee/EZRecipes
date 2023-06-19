using Common.Constants.CI;

namespace Common.DX.CI
{
    public static class Recipe
	{
		public static readonly HashSet<string> Factories = new();
		public static readonly HashSet<string> Fluids = new();

		private static RecipeCount GetRecipeCounts(Input.CI.Recipe recipe, RecipeConfig recipeConfig)
		{
			Input.CI.Ingredient[] ingredients = recipe.JS_LibValue.mIngredients.JS_Values;
			Input.CI.Product[] products = recipe.JS_LibValue.mProduct.JS_Values;

			HashSet<IngredientProduct> recipeCounts = new();

			if (recipeConfig.CustomRecipeCount.ContainsKey(recipe.JS_LibOuter))
			{
				return recipeConfig.CustomRecipeCount[recipe.JS_LibOuter];
			}
			else if (recipe.JS_LibValue.mProduct.JS_Values[0].ItemClass == recipeConfig.Protein)
			{
				var lists = Enumerable.Repeat(1, 10).ToList();

				return new (1, ingredients.Length, 2, products.Length);
			}
			else if (recipe.JS_LibValue.mIngredients.JS_Values.Any(r => recipeConfig.SlugIngredientCount.ContainsKey(r.ItemClass)))
			{
				return new (1, ingredients.Length, recipeConfig.SlugIngredientCount[recipe.JS_LibValue.mIngredients.JS_Values[0].ItemClass], products.Length);
			}
			else
			{
				foreach (var producedIn in recipe.JS_LibValue.mProducedIn.JS_Values)
				{
					if (recipeConfig.FactoryIngredientProductCount.TryGetValue(producedIn, out var parameters))
					{
						recipeCounts.Add(parameters);
					}
				}
			}

			var bestRecipeCount = recipeCounts.OrderBy(ip => ip.Product / (double)ip.Ingredient).First();

			return new (bestRecipeCount.Ingredient, ingredients.Length, bestRecipeCount.Product, products.Length);
		}

		private static double GetManufacturingDuration(Input.CI.Recipe recipe, RecipeConfig recipeConfig)
		{
			var manufacturingDurations = new HashSet<double>();

			foreach (var producedIn in recipe.JS_LibValue.mProducedIn.JS_Values)
			{
				if (recipeConfig.FactoryDuration.TryGetValue(producedIn, out var duration))
				{
					manufacturingDurations.Add(duration);
				}
			}

			return manufacturingDurations.Any() ? manufacturingDurations.Min() : 1;
		}

		public static Output.CI.Recipe? From(Input.CI.Recipe recipe, RecipeConfig recipeConfig)
		{
			if (null == recipe.JS_LibValue.mProducedIn?.JS_Values || !recipe.JS_LibValue.mProducedIn.JS_Values.Any(recipeConfig.WhiteListedFactories.Contains))
			{
				return null;
			}
			else
			{
				RecipeCount recipeCount = GetRecipeCounts(recipe, recipeConfig);

				IEnumerable<Output.CI.Ingredient>? ingredients = null != recipe.JS_LibValue.mIngredients ? Ingredient.From(recipeConfig, recipe, recipeCount) : null;
				IEnumerable<Output.CI.Product>? products = null != recipe.JS_LibValue.mProduct ? Product.From(recipeConfig, recipe, recipeCount) : null;

				if (null != ingredients && null != products)
				{
					return new Output.CI.Recipe
					{
						ResourcePath = $"/{recipe.JS_LibOuter.Replace("Default__", "")}",
						Name = $"{recipe.JS_LibValue.mDisplayName}",
						FileName = recipe.JS_LibOuter[recipe.JS_LibOuter.LastIndexOf(".")..].Replace(".Default__", ""),
						IngredientCount = string.Join(", ", recipeCount.Inputs),
						ProductCount = string.Join(", ", recipeCount.Outputs),
						Ingredients = ingredients.ToList(),
						Products = products.ToList(),
						ManufacturingDuration = GetManufacturingDuration(recipe, recipeConfig),
						ManualManufacturingMultiplier = recipe.JS_LibValue?.mManualManufacturingMultiplier
					};
				}
				else
				{
					return null;
				}
			}
		}
	}
}
