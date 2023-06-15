namespace Common.DX.FRM
{
    public class Product
    {
        public static Output.FRM.Product From(Input.FRM.Product product, int? customQuantity = null)
        {
            return new Output.FRM.Product()
            {
                Item = product.ClassName,
                Amount = (customQuantity ?? product.Amount) * (Constants.FRM.Recipe.Fluids.Contains(product.ClassName!) ? 1000 : 1)
            };
        }

        public static IEnumerable<Output.FRM.Product> From(List<Input.FRM.Product>? products, int? customQuantity = null)
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
