namespace EZRecipes.DX
{
	public static class Ingredient
	{
		public static Output.Ingredient From(Input.Ingredient ingredient, int? customQuantity = null)
		{
			return new Output.Ingredient()
			{
				Item = ingredient.ClassName,
				Amount = (customQuantity ?? ingredient.Amount) * (Constants.Recipe.Fluids.Contains(ingredient.ClassName!) ? 1000 : 1)
			};
		}

		public static IEnumerable<Output.Ingredient> From(List<Input.Ingredient>? ingredients, int? customQuantity = null)
		{
			if (null != ingredients)
			{
				foreach (var ingredient in ingredients)
				{
					yield return From(ingredient, customQuantity);
				}
			}
		}
	}
}
