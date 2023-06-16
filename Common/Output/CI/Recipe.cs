using Newtonsoft.Json;

namespace Common.Output.CI
{
	public class Recipe
	{
		[JsonIgnore]
		public required string ResourcePath { get; set; }

		[JsonProperty("$schema")]
		public static string Schema { get; set; } = "https://raw.githubusercontent.com/budak7273/ContentLib_Documentation/main/JsonSchemas/CL_Recipe.json";

		[JsonIgnore]
		public required string Name { get; set; }

		[JsonIgnore]
		public required string FileName { get; set; }

		[JsonIgnore]
		public int IngredientCount { get; set; }

		[JsonIgnore]
		public int ProductCount { get; set; }

		public required List<Ingredient> Ingredients { get; set; }

		public required List<Product> Products { get; set; }

		public double? ManufacturingDuration { get; set; }

		public double? ManualManufacturingMultiplier { get; set; }
	}
}
