using Common.Constants.CI;

namespace Common.DX.CI
{
    public static class Ingredient
	{
		public static Output.CI.Ingredient From(RecipeConfig recipeConfig, Input.CI.Ingredient ingredient, int count)
		{
			if (recipeConfig.Fluids.Contains(ingredient.ItemClass))
			{
				count *= 1000;
			}

			return new Output.CI.Ingredient
			{
				Item = ingredient.ItemClass,
				Amount = count
			};
		}

		public static IEnumerable<Output.CI.Ingredient> From(RecipeConfig recipeConfig, Input.CI.Recipe recipe, RecipeCount recipeCount)
		{
			var ingredients = recipe.JS_LibValue.mIngredients.JS_Values;

			if (recipeCount.Inputs.Count() != ingredients.Length)
			{
				throw new Exception("Custom recipe ingredient count mismatch");
			}

			for (int i = 0; i < ingredients.Length; i++)
			{
				yield return From(recipeConfig, ingredients[i], recipeCount.Inputs.ElementAt(i));
			}
		}
	}
}