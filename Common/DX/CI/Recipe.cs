namespace Common.DX.CI
{
	public static class Recipe
	{
		public static readonly HashSet<string> Factories = new();
		public static readonly HashSet<string> Fluids = new();

		private static Tuple<int, int> GetRecipeCounts(Input.CI.Recipe recipe)
		{
			HashSet<Tuple<int, int>> recipeCounts = new();

			if (recipe.JS_LibValue.mProduct.JS_Values[0].ItemClass == Constants.CI.Recipes.Protein)
			{
				return new Tuple<int, int>(1, 2);
			}
			else if (recipe.JS_LibValue.mIngredients.JS_Values.Any(r => Constants.CI.Recipes.SlugIngredient.ContainsKey(r.ItemClass)))
			{
				return new Tuple<int, int>(1, Constants.CI.Recipes.SlugIngredient[recipe.JS_LibValue.mIngredients.JS_Values[0].ItemClass]);
			}
			else
			{
				foreach (var producedIn in recipe.JS_LibValue.mProducedIn.JS_Values)
				{
					if (Constants.CI.Recipes.FactoryIngredientProductCount.TryGetValue(producedIn, out var parameters))
					{
						recipeCounts.Add(parameters);
					}
				}
			}

			return recipeCounts.OrderBy(ip => ip.Item2 / (double)ip.Item1).First();
		}

		private static double GetManufacturingDuration(Input.CI.Recipe recipe)
		{
			var manufacturingDurations = new HashSet<double>();

			foreach (var producedIn in recipe.JS_LibValue.mProducedIn.JS_Values)
			{
				if (Constants.CI.Recipes.FactoryDuration.TryGetValue(producedIn, out var duration))
				{
					manufacturingDurations.Add(duration);
				}
			}

			return manufacturingDurations.Any() ? manufacturingDurations.Min() : 1;
		}

		public static Output.CI.Recipe? From(Input.CI.Recipe recipe)
		{
			if (null == recipe.JS_LibValue.mProducedIn?.JS_Values || !recipe.JS_LibValue.mProducedIn.JS_Values.Any(Constants.CI.Recipes.WhiteListedFactories.Contains))
			{
				return null;
			}
			else
			{
				Tuple<int, int> recipeCounts = GetRecipeCounts(recipe);

				IEnumerable<Output.CI.Ingredient>? ingredients = null != recipe.JS_LibValue.mIngredients ? Ingredient.From(recipe.JS_LibValue.mIngredients, recipeCounts.Item1) : null;
				IEnumerable<Output.CI.Product>? products = null != recipe.JS_LibValue.mProduct ? Product.From(recipe.JS_LibValue.mProduct, recipeCounts.Item2) : null;

				if (null != ingredients && null != products)
				{
					return new Output.CI.Recipe
					{
						ResourcePath = $"/{recipe.JS_LibOuter.Replace("Default__", "")}",
						Name = $"{recipe.JS_LibValue.mDisplayName}",
						FileName = recipe.JS_LibOuter[recipe.JS_LibOuter.LastIndexOf(".")..].Replace(".Default__", ""),
						IngredientCount = recipeCounts.Item1,
						ProductCount = recipeCounts.Item2,
						Ingredients = ingredients.ToList(),
						Products = products.ToList(),
						ManufacturingDuration = GetManufacturingDuration(recipe),
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
