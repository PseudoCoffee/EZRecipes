﻿namespace Common.Constants.CI
{
	public class RecipeCount
	{
		public IEnumerable<int> Inputs { get; set; }

		public IEnumerable<int> Outputs { get; set; }

		public double? CustomDurationConstant { get; set; } = null;

		public double? CustomDurationMultiplier { get; set; } = null;

		public RecipeCount()
		{
			Inputs = new List<int>();
			Outputs = new List<int>();
		}

		public RecipeCount(int inputValue, int inputCount, int outputValue, int outputCount, double? customDurationConstant = null, double? customDurationMultiplier = null)
		{
			Inputs = Enumerable.Repeat(inputValue, inputCount).ToList();
			Outputs = Enumerable.Repeat(outputValue, outputCount).ToList();
			CustomDurationConstant = customDurationConstant;
			CustomDurationMultiplier = customDurationMultiplier;
		}

		public RecipeCount(IEnumerable<int> inputs, IEnumerable<int> outputs, double? customDuration = null)
		{
			Inputs = inputs;
			Outputs = outputs;
			CustomDurationConstant = customDuration;
		}
	}

	public class IngredientProduct
	{
		public IngredientProduct(int ingredient, int product)
		{
			Ingredient = ingredient;
			Product = product;
		}

		public int Ingredient { get; set; }

		public int Product { get; set; }
	}

	public class RecipeConfig
	{
		public required string Protein { get; set; }

		public required Dictionary<string, int> SlugIngredientCount { get; set; }

		public required List<string> Fluids { get; set; }

		public required List<string> AllFactories { get; set; }

		public required List<string> WhiteListedFactories { get; set; }

		public required Dictionary<string, double> FactoryDuration { get; set; }

		public required Dictionary<string, FactoryVariablePower> FactoryVariablePower { get; set; }

		public required Dictionary<string, GenericFactoryRecipe> CustomGenericFactoryRecipe { get; set; }
		
		public required Dictionary<string, RecipeCount> CustomRecipeCount { get; set; }

		public required Dictionary<string, IngredientProduct> FactoryIngredientProductCount { get; set; }

		public required List<string> BlackListedFactories { get; set; }
	}
}
