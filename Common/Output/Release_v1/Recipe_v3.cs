using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Output.Release_v1
{
	public class Recipe_v3
	{
		[JsonIgnore]
		public required string AssetPath { get; set; }
		
		[JsonIgnore]
		public required string InternalName { get; set; }

		[JsonProperty("$schema")]
		public static string Schema { get; set; } = "https://raw.githubusercontent.com/budak7273/ContentLib_Documentation/main/JsonSchemas/CL_Recipe.json";

		public string? Name { get; set; }

		public required Ingredient[] Ingredients { get; set; }

		public required Product[] Products { get; set; }

		public double? ManufacturingDuration { get; set; }

		public string[]? ProducedIn { get; set; }

		public string? OverrideCategory { get; set; }

		public double? ManufacturingMenuPriority { get; set; }

		public string[]? UnlockedBy { get; set; }

		public double? ManualManufacturingMultiplier { get; set; }

		public double? VariablePowerConsumptionConstant { get; set; }

		public double? VariablePowerConsumptionFactor { get; set; }

		public bool? ClearIngredients { get; set; }

		public bool? ClearProducts { get; set; }

		public bool? ClearBuilders { get; set; }

		public class Ingredient
		{
			[JsonIgnore]
			public bool? MatchAmount { get; set; }
			public required string Item { get; set; }
			public required int Amount { get; set; }
		}

		public class Product
		{
			public bool? MatchAmount { get; set; }
			public required string Item { get; set; }
			public required int Amount { get; set; }
		}
	}
}
