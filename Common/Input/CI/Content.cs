namespace Common.Input.CI
{
	public class Content
	{
		public bool mDisplayNameOverride { get; set; }

		public string mDisplayName { get; set; }

		public Mingredients mIngredients { get; set; }

		public Mproducts mProduct { get; set; }

		public string mOverriddenCategory { get; set; }

		public double mManufacturingMenuPriority { get; set; }

		public double mManufactoringDuration { get; set; }

		public double mManualManufacturingMultiplier { get; set; }

		public Mproducedin mProducedIn { get; set; }

		public string mMaterialCustomizationRecipe { get; set; }

		public Mrelevantevents mRelevantEvents { get; set; }

		public double mVariablePowerConsumptionConstant { get; set; }

		public double mVariablePowerConsumptionFactor { get; set; }

	}
}
