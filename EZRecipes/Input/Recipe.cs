namespace EZRecipes.Input
{
    public class Recipe
    {
        public string? RecipeName { get; set; }

        public string? ClassName { get; set; }

        public string? Category { get; set; }

        public List<object>? Events { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public List<Product>? Products { get; set; }

        public List<ProducedInContainer>? ProducedIn { get; set; }

        public double ManualDuration { get; set; }

        public int ManufacturingDuration { get; set; }
    }
}
