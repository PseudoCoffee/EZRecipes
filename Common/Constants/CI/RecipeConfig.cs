namespace Common.Constants.CI
{
	public class RecipeCount
	{
		public List<int> Inputs { get; set; }

		public List<int> Outputs { get; set; }

		public RecipeCount()
		{
			Inputs = new List<int>();
			Outputs = new List<int>();
		}

		public RecipeCount(int inputValue, int inputCount, int outputValue, int outputCount)
		{
			Inputs = Enumerable.Repeat(inputValue, inputCount).ToList();
			Outputs = Enumerable.Repeat(outputValue, outputCount).ToList();
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

		public required Dictionary<string, RecipeCount> CustomRecipeCount { get; set; }

		public required Dictionary<string, IngredientProduct> FactoryIngredientProductCount { get; set; }

		public required List<string> BlackListedFactories { get; set; }
	}
}
