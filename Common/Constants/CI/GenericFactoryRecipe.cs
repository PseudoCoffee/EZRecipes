using Common.Output.Release_v1;

namespace Common.Constants.CI
{
	public class GenericFactoryRecipe
	{
		public required double? CustomDurationConstant { get; set; }

		public required Recipe_v3.Ingredient[] Ingredients { get; set; }

		public required Recipe_v3.Product[] Products { get; set; }

	}
}
