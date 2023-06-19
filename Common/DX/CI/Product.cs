using Common.Constants.CI;

namespace Common.DX.CI
{
    public static class Product
	{
		public static Output.CI.Product From(RecipeConfig recipeConfig, Input.CI.Product product, int count)
		{
			if (recipeConfig.Fluids.Contains(product.ItemClass))
			{
				count *= 1000;
			}

			return new Output.CI.Product
			{
				Item = product.ItemClass,
				Amount = count
			};
		}

		public static IEnumerable<Output.CI.Product> From(RecipeConfig recipeConfig, Input.CI.Recipe recipe, RecipeCount recipeCount)
		{
			var products = recipe.JS_LibValue.mProduct.JS_Values;

			if (recipeCount.Outputs.Count != products.Length)
			{
				throw new Exception("Custom recipe product count mismatch");
			}

			for (int i = 0; i < products.Length; i++)
			{
				yield return From(recipeConfig, products[i], recipeCount.Outputs[i]);
			}
		}
	}
}