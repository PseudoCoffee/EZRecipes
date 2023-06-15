namespace Common.DX.FRM
{
    public static class Recipe
    {
        public static Output.FRM.Recipe? From(string unlockedBy, int techTier, Input.FRM.Recipe recipe)
        {
            List<string>? producedIn = ProducedIn.From(recipe.ProducedIn);

            if (null != producedIn)
            {
                bool isPackager = producedIn!.Contains("Build_Packager");

                float durationModifier = 4f / 6f;
                float duration = durationModifier * (techTier / 4 + 1);
                int recipeCount = 4;
                int ingredientCount = isPackager ? 1 : recipeCount;
                int productCount = isPackager ? 1 : recipeCount * 3 / 4;

                List<Output.FRM.Ingredient> ingredients = Ingredient.From(recipe.Ingredients, ingredientCount).ToList();
                List<Output.FRM.Product> products = Product.From(recipe.Products, productCount).ToList();

                return new Output.FRM.Recipe
                {
                    Name = recipe.RecipeName,
                    FileName = recipe.ClassName ?? throw new ArgumentNullException(nameof(recipe.ClassName)),
                    Ingredients = ingredients,
                    Products = products,
                    ProducedIn = producedIn,
                    OverrideCategory = recipe.Category,
                    ManufacturingDuration = duration,
                    ManualManufacturingMultiplier = recipe.ManualDuration / recipe.ManufacturingDuration,
                    UnlockedBy = new List<string> { unlockedBy },
                    VariablePowerConsumptionFactor = null,
                    VariablePowerConsumptionConstant = null,
                    ManufacturingMenuPriority = null,
                    OverrideName = false,
                    ClearIngredients = ingredients.Any(),
                    ClearProducts = products.Any(),
                    ClearBuilders = producedIn.Any(),
                };
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<Output.FRM.Recipe?> From(string unlockedBy, int techTier, List<Input.FRM.Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                yield return From(unlockedBy, techTier, recipe);
            }
        }
    }
}
