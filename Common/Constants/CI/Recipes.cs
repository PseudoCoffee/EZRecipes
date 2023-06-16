namespace Common.Constants.CI
{
	public static class Recipes
	{
		#region Factories

		public static readonly string Assembler = "/Game/FactoryGame/Buildable/Factory/AssemblerMk1/Build_AssemblerMk1.Build_AssemblerMk1_C";
		public static readonly string Blender = "/Game/FactoryGame/Buildable/Factory/Blender/Build_Blender.Build_Blender_C";
		public static readonly string Constructor = "/Game/FactoryGame/Buildable/Factory/ConstructorMk1/Build_ConstructorMk1.Build_ConstructorMk1_C";
		public static readonly string Foundry = "/Game/FactoryGame/Buildable/Factory/FoundryMk1/Build_FoundryMk1.Build_FoundryMk1_C";
		public static readonly string ParticleAccelerator = "/Game/FactoryGame/Buildable/Factory/HadronCollider/Build_HadronCollider.Build_HadronCollider_C";
		public static readonly string Manufacturer = "/Game/FactoryGame/Buildable/Factory/ManufacturerMk1/Build_ManufacturerMk1.Build_ManufacturerMk1_C";
		public static readonly string OilRefinery = "/Game/FactoryGame/Buildable/Factory/OilRefinery/Build_OilRefinery.Build_OilRefinery_C";
		public static readonly string Packager = "/Game/FactoryGame/Buildable/Factory/Packager/Build_Packager.Build_Packager_C";
		public static readonly string Smelter = "/Game/FactoryGame/Buildable/Factory/SmelterMk1/Build_SmelterMk1.Build_SmelterMk1_C";

		#endregion

		#region Fluids

		public static readonly string Water = "/Game/FactoryGame/Resource/RawResources/Water/Desc_Water.Desc_Water_C";
		public static readonly string Oil = "/Game/FactoryGame/Resource/RawResources/CrudeOil/Desc_LiquidOil.Desc_LiquidOil_C";
		public static readonly string HeavyOilResidue = "/Game/FactoryGame/Resource/Parts/HeavyOilResidue/Desc_HeavyOilResidue.Desc_HeavyOilResidue_C";
		public static readonly string Fuel = "/Game/FactoryGame/Resource/Parts/Fuel/Desc_LiquidFuel.Desc_LiquidFuel_C";
		public static readonly string Turbofuel = "/Game/FactoryGame/Resource/Parts/Turbofuel/Desc_LiquidTurboFuel.Desc_LiquidTurboFuel_C";
		public static readonly string LiquidBiofuel = "/Game/FactoryGame/Resource/Parts/BioFuel/Desc_LiquidBiofuel.Desc_LiquidBiofuel_C";
		public static readonly string AluminaSolution = "/Game/FactoryGame/Resource/Parts/Alumina/Desc_AluminaSolution.Desc_AluminaSolution_C";
		public static readonly string SulfuricAcid = "/Game/FactoryGame/Resource/Parts/SulfuricAcid/Desc_SulfuricAcid.Desc_SulfuricAcid_C";
		public static readonly string Nitrogen = "/Game/FactoryGame/Resource/RawResources/NitrogenGas/Desc_NitrogenGas.Desc_NitrogenGas_C";
		public static readonly string NitricAcid = "/Game/FactoryGame/Resource/Parts/NitricAcid/Desc_NitricAcid.Desc_NitricAcid_C";

		#endregion

		#region Slugs

		public static readonly string BlueSlug = "/Game/FactoryGame/Resource/Environment/Crystal/Desc_Crystal.Desc_Crystal_C";
		public static readonly string YellowSlug = "/Game/FactoryGame/Resource/Environment/Crystal/Desc_Crystal_mk2.Desc_Crystal_mk2_C";
		public static readonly string PurpleSlug = "/Game/FactoryGame/Resource/Environment/Crystal/Desc_Crystal_mk3.Desc_Crystal_mk3_C";

		#endregion

		public static readonly string Protein = "/Game/FactoryGame/Resource/Parts/AlienProtein/Desc_AlienProtein.Desc_AlienProtein_C";

		public static readonly Dictionary<string, int> SlugIngredient = new()
		{
			{ BlueSlug, 1 },
			{ YellowSlug, 2 },
			{ PurpleSlug, 5 },
		};

		public static readonly List<string> Fluids = new()
		{
			Water, Oil, HeavyOilResidue, Fuel, Turbofuel, LiquidBiofuel, AluminaSolution, SulfuricAcid, Nitrogen, NitricAcid
		};

		public static readonly List<string> AllFactories = new()
		{
			"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkBenchComponent.BP_WorkBenchComponent_C",
			"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C",
			Assembler,
			"/Game/FactoryGame/Buildable/Factory/AutomatedWorkBench/Build_AutomatedWorkBench.Build_AutomatedWorkBench_C",
			Blender,
			Constructor,
			"/Game/FactoryGame/Buildable/Factory/Converter/Build_Converter.Build_Converter_C",
			Foundry,
			ParticleAccelerator,
			Manufacturer,
			OilRefinery,
			Packager,
			Smelter,
			"/Script/FactoryGame.FGBuildGun",
			"/Script/FactoryGame.FGBuildableAutomatedWorkBench",
		};

		public static readonly List<string> WhiteListedFactories = new()
		{
			Assembler, Blender, Constructor, Foundry, ParticleAccelerator, Manufacturer, OilRefinery, Packager, Smelter
		};

		public static readonly Dictionary<string, double> FactoryDuration = new()
		{
			{ Packager, 0.5 },

			{ Constructor, 0.5 },
			{ Smelter, 0.5 },

			{ Assembler, 1.5 },
			{ Foundry, 1.5 },

			{ Blender, 2.0 },
			{ ParticleAccelerator, 2 },
			{ OilRefinery, 1},

			{ Manufacturer, 3 },
		};

		public static readonly Dictionary<string, Tuple<int, int>> FactoryIngredientProductCount = new()
		{
			{ Packager, new Tuple<int, int>(1, 1) },

			{ Constructor, new Tuple<int, int>(2, 2) },
			{ Smelter, new Tuple<int, int>(2, 2) },
			
			{ Assembler, new Tuple<int, int>(4, 3) },
			{ Foundry, new Tuple<int, int>(4, 3) },
			
			{ Blender, new Tuple<int, int>(4, 3) },
			{ ParticleAccelerator, new Tuple<int, int>(4, 1) },
			{ OilRefinery, new Tuple<int, int>(2, 1) },
			
			{ Manufacturer, new Tuple<int, int>(4, 2) },
		};

		public static readonly List<string> BlackListedFactories = new()
		{
			"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C",
			"/Script/FactoryGame.FGBuildGun",
		};
	}
}
