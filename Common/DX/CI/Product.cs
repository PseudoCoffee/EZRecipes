namespace Common.DX.CI
{
	public static class Product
	{
		public static Output.CI.Product From(Input.CI.Product product, int amount)
		{
			if (Constants.CI.Recipes.Fluids.Contains(product.ItemClass))
			{
				amount *= 1000;
			}

			return new Output.CI.Product
			{
				Item = product.ItemClass,
				Amount = amount
			};
		}

		public static IEnumerable<Output.CI.Product> From(Input.CI.Mproducts products, int amount)
		{
			foreach (var product in products.JS_Values)
			{
				yield return From(product, amount);
			}
		}
	}
}
