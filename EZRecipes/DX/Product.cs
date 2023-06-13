namespace EZRecipes.DX
{
	public class Product
	{
		public static Output.Product From(Input.Product product, int? customQuantity = null)
		{
			return new Output.Product()
			{
				Item = product.ClassName,
				Amount = (customQuantity ?? product.Amount) * (Constants.Recipe.Fluids.Contains(product.ClassName!) ? 1000 : 1)
			};
		}

		public static IEnumerable<Output.Product> From(List<Input.Product>? products, int? customQuantity = null)
		{
			if (null != products)
			{
				foreach (var Product in products)
				{
					yield return From(Product, customQuantity);
				}
			}
		}
	}
}
