namespace Common.DX.CI
{
	public static class Product
	{
		public static Output.CI.Product From(Input.CI.Product ingredient)
		{
			return new Output.CI.Product
			{
				Item = ingredient.ItemClass,
				Amount = 69//ingredient.Amount
			};
		}

		public static IEnumerable<Output.CI.Product> From(Input.CI.Mproducts products)
		{
			foreach (var product in products.JS_Values)
			{
				yield return From(product);
			}
		}
	}
}
