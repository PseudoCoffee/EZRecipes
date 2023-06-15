namespace Common.DX.FRM
{
    public static class Ingredient
    {
        public static Output.FRM.Ingredient From(Input.FRM.Ingredient ingredient, int? customQuantity = null)
        {
            return new Output.FRM.Ingredient()
            {
                Item = ingredient.ClassName,
                Amount = (customQuantity ?? ingredient.Amount) * (Constants.Recipe.Fluids.Contains(ingredient.ClassName!) ? 1000 : 1)
            };
        }

        public static IEnumerable<Output.FRM.Ingredient> From(List<Input.FRM.Ingredient>? ingredients, int? customQuantity = null)
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
