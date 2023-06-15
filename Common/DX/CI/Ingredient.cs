namespace Common.DX.CI
{
	public static class Ingredient
	{
		public static Output.CI.Ingredient From(Input.CI.Ingredient ingredient)
		{
			return new Output.CI.Ingredient
			{ 
				Item = ingredient.ItemClass,
				Amount = 420//ingredient.Amount
			};
		}

		public static IEnumerable<Output.CI.Ingredient> From(Input.CI.Mingredients ingredients)
		{
			foreach(var ingredient in ingredients.JS_Values)
			{
				yield return From(ingredient);
			}
		}
	}
}
