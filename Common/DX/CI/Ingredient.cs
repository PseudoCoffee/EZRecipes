namespace Common.DX.CI
{
	public static class Ingredient
	{
		public static Output.CI.Ingredient From(Input.CI.Ingredient ingredient, int amount)
		{
			if (Constants.CI.Recipes.Fluids.Contains(ingredient.ItemClass))
			{
				amount *= 1000;
			}

			return new Output.CI.Ingredient
			{
				Item = ingredient.ItemClass,
				Amount = amount
			};
		}

		public static IEnumerable<Output.CI.Ingredient> From(Input.CI.Mingredients ingredients, int amount)
		{
			foreach (var ingredient in ingredients.JS_Values)
			{
				yield return From(ingredient, amount);
			}
		}
	}
}
