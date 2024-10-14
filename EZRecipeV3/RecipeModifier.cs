using Common.Constants.CI;
using Common.Output.Release_v1;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZRecipeV3
{
	public static class RecipeModifier
	{
		public static readonly HashSet<string> Factories = new();
		public static readonly HashSet<string> Fluids = new();

		private static bool TryGetCustomRecipeByFactory(Recipe_v3 recipe, RecipeConfig recipeConfig, [NotNullWhen(returnValue: true)] out RecipeCount? recipeCount)
		{
			if (null != recipe.ProducedIn)
			{
				HashSet<GenericFactoryRecipe> genericFactoryRecipes = new();

				foreach (var factory in recipe!.ProducedIn)
				{
					if (recipeConfig.CustomGenericFactoryRecipe.TryGetValue(factory, out GenericFactoryRecipe? genericFactoryRecipe))
					{
						if (genericFactoryRecipe.Ingredients.Length == recipe.Ingredients.Length && genericFactoryRecipe.Products.Length == recipe.Products.Length)
						{
							bool valid = true;
							int genericIngredients = genericFactoryRecipe.Ingredients.Count(i => i.Item == "*");
							int genericProducts = genericFactoryRecipe.Products.Count(p => p.Item == "*");

							foreach (var ingredient in genericFactoryRecipe.Ingredients)
							{
								var matchingIngredient = recipe.Ingredients.FirstOrDefault(i => i.Item == ingredient.Item);
								if ((matchingIngredient == null || matchingIngredient.Amount != ingredient.Amount) && genericIngredients-- <= 0)
								{
									valid = false;
								}
							}

							foreach (var product in genericFactoryRecipe.Products)
							{
								var matchingProduct = recipe.Products.FirstOrDefault(i => i.Item == product.Item);
								if ((matchingProduct == null || matchingProduct.Amount != product.Amount) && genericProducts-- <= 0)
								{
									valid = false;
								}
							}

							if (valid)
							{
								genericFactoryRecipes.Add(genericFactoryRecipe);
							}
						}
					}
				}

				if (genericFactoryRecipes.Count != 0)
				{
					GenericFactoryRecipe bestCandidate = genericFactoryRecipes
						.OrderBy(gfr => gfr.Products.Max(p => p.Amount) / (double)gfr.Ingredients.Max(i => i.Amount))
						.First();

					recipeCount = new RecipeCount(bestCandidate.Ingredients.Select(i => i.Amount), bestCandidate.Products.Select(p => p.Amount));
					return true;
				}
			}

			recipeCount = null;
			return false;
		}

		private static RecipeCount GetRecipeCounts(Recipe_v3 recipe, RecipeConfig recipeConfig)
		{
			HashSet<IngredientProduct> recipeCounts = new();
			RecipeCount? recipeCount;

			string? firstKey = recipe.ProducedIn!.FirstOrDefault(recipeConfig.CustomGenericFactoryRecipe.ContainsKey);

			if (recipeConfig.CustomRecipeCount.TryGetValue(recipe.InternalName, out recipeCount))
			{
				return recipeCount;
			}
			else if (TryGetCustomRecipeByFactory(recipe, recipeConfig, out recipeCount))
			{
				return recipeCount;
			}
			else if (recipe.Ingredients.Any(r => recipeConfig.SlugIngredientCount.ContainsKey(r.Item)))
			{
				return new(1, recipe.Ingredients.Length, recipeConfig.SlugIngredientCount[recipe.Ingredients[0].Item], recipe.Products.Length);
			}
			else
			{
				foreach (var producedIn in recipe.ProducedIn!)
				{
					if (recipeConfig.FactoryIngredientProductCount.TryGetValue(producedIn, out var parameters))
					{
						recipeCounts.Add(parameters);
					}
				}
			}

			var bestRecipeCount = recipeCounts.OrderBy(ip => ip.Product / (double)ip.Ingredient).First();

			return new(bestRecipeCount.Ingredient, recipe.Ingredients.Length, bestRecipeCount.Product, recipe.Products.Length);
		}

		private static double? GetManufacturingDuration(Recipe_v3 recipe, RecipeConfig recipeConfig)
		{
			var manufacturingDurations = new HashSet<double>();

			foreach (var producedIn in recipe.ProducedIn!)
			{
				if (recipeConfig.FactoryDuration.TryGetValue(producedIn, out var duration))
				{
					manufacturingDurations.Add(duration);
				}
			}

			return manufacturingDurations.Any() ? manufacturingDurations.Min() : recipe.ManufacturingDuration;
		}

		public static Recipe_v3? Modify(Recipe_v3 recipe, RecipeConfig recipeConfig)
		{
			if (null == recipe.ProducedIn || !recipe.ProducedIn.Any(recipeConfig.WhiteListedFactories.Contains))
			{
				return null;
			}
			else
			{
				RecipeCount recipeCount = GetRecipeCounts(recipe, recipeConfig);

				IEnumerable<Recipe_v3.Ingredient>? ingredients = IngredientModifier.ModifyIngredients(recipeConfig, recipe, recipeCount);
				IEnumerable<Recipe_v3.Product>? products = ProductModifier.ModifyProducts(recipeConfig, recipe, recipeCount);

				if (null != ingredients && null != products)
				{
					return new Recipe_v3
					{
						AssetPath = recipe.AssetPath,
						InternalName = recipe.InternalName,
						Ingredients = ingredients.ToArray(),
						Products = products.ToArray(),
						ManufacturingDuration = recipeCount.CustomDuration ?? GetManufacturingDuration(recipe, recipeConfig),
						ManualManufacturingMultiplier = recipe.ManualManufacturingMultiplier
					};
				}
				else
				{
					return null;
				}
			}
		}

		private static class IngredientModifier
		{
			private static Recipe_v3.Ingredient ModifyIngredient(RecipeConfig recipeConfig, Recipe_v3.Ingredient ingredient, int count)
			{
				if (recipeConfig.Fluids.Contains(ingredient.Item))
				{
					count *= 1000;
				}

				return new Recipe_v3.Ingredient
				{
					Item = ingredient.Item,
					Amount = count
				};
			}

			public static IEnumerable<Recipe_v3.Ingredient> ModifyIngredients(RecipeConfig recipeConfig, Recipe_v3 recipe, RecipeCount recipeCount)
			{
				if (recipe.Ingredients.Length != recipeCount.Inputs.Count())
				{
					throw new Exception("Custom recipe ingredient count mismatch");
				}

				for (int i = 0; i < recipe.Ingredients.Length; i++)
				{
					yield return ModifyIngredient(recipeConfig, recipe.Ingredients[i], recipeCount.Inputs.ElementAt(i));
				}
			}
		}

		private static class ProductModifier
		{
			private static Recipe_v3.Product ModifyProduct(RecipeConfig recipeConfig, Recipe_v3.Product Product, int count)
			{
				if (recipeConfig.Fluids.Contains(Product.Item))
				{
					count *= 1000;
				}

				return new Recipe_v3.Product
				{
					Item = Product.Item,
					Amount = count
				};
			}

			public static IEnumerable<Recipe_v3.Product> ModifyProducts(RecipeConfig recipeConfig, Recipe_v3 recipe, RecipeCount recipeCount)
			{
				if (recipe.Products.Length != recipeCount.Outputs.Count())
				{
					throw new Exception("Custom recipe Product count mismatch");
				}

				for (int i = 0; i < recipe.Products.Length; i++)
				{
					yield return ModifyProduct(recipeConfig, recipe.Products[i], recipeCount.Outputs.ElementAt(i));
				}
			}
		}
	}
}
