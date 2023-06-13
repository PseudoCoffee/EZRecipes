using Newtonsoft.Json;

namespace EZRecipes.Output
{
	public class Recipe
	{
		[JsonProperty("$schema")]
		public static string Schema { get; set; } = "https://raw.githubusercontent.com/budak7273/ContentLib_Documentation/main/JsonSchemas/CL_Recipe.json";

		public string? Name { get; set; }

		[JsonIgnore]
		public required string FileName { get; set; }

		public List<Ingredient>? Ingredients { get; set; }

		public List<Product>? Products { get; set; }

		public List<string>? ProducedIn { get; set; }

		public string? OverrideCategory { get; set; }

		public double? ManufacturingDuration { get; set; }

		public double? ManualManufacturingMultiplier { get; set; }

		public List<string>? UnlockedBy { get; set; }

		public double? VariablePowerConsumptionFactor { get; set; }

		public double? VariablePowerConsumptionConstant { get; set; }

		public double? ManufacturingMenuPriority { get; set; }

		public bool? OverrideName { get; set; }

		public bool? ClearIngredients { get; set; }

		public bool? ClearProducts { get; set; }

		public bool? ClearBuilders { get; set; }
	}
}
