namespace Common.Input.FRM
{
    public class RecipeGroup
    {
        public string? Name { get; set; }

        public string ClassName { get; set; } = string.Empty;

        public int? TechTier { get; set; }

        public string? Type { get; set; }

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public bool? Locked { get; set; }

        public bool? Purchased { get; set; }

        public bool? DepLocked { get; set; }
    }
}
