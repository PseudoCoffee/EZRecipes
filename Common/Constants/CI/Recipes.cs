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

			{ Constructor, 1.0 },
			{ Smelter, 1.0 },

			{ Assembler, 1.5 },
			{ Foundry, 1.5 },

			{ Blender, 2.0 },
			{ ParticleAccelerator, 2 },
			{ OilRefinery, 2},

			{ Manufacturer, 3 },
		};

		//public static readonly List<Tuple<string, double>> FactoryDuration = new()
		//{
		//	new Tuple<string, double> (Packager, 0.5 ),

		//	new Tuple<string, double> (Constructor, 1.0 ),
		//	new Tuple<string, double> (Smelter, 1.0 ),

		//	new Tuple<string, double> (Assembler, 1.5 ),
		//	new Tuple<string, double> (Foundry, 1.5 ),

		//	new Tuple<string, double> (Blender, 2.0 ),
		//	new Tuple<string, double> (ParticleAccelerator, 2 ),
		//	new Tuple<string, double> (OilRefinery, 2),

		//	new Tuple<string, double> (Manufacturer, 3 ),
		//};

		public static readonly Dictionary<string, Tuple<int, int>> FactoryIngredientProductCount = new()
		{
			{ Assembler, new Tuple<int, int>(4, 3) },
			{ Blender, new Tuple<int, int>(4, 3) },
			{ Constructor, new Tuple<int, int>(4, 4) },
			{ Foundry, new Tuple<int, int>(4, 3) },
			{ ParticleAccelerator, new Tuple<int, int>(4, 1) },
			{ Manufacturer, new Tuple<int, int>(4, 2) },
			{ OilRefinery, new Tuple<int, int>(4, 2) },
			{ Packager, new Tuple<int, int>(1, 1) },
			{ Smelter, new Tuple<int, int>(4, 4) },
		};

		//public static readonly List<Tuple<string, Tuple<int, int>>> FactoryIngredientProductCount = new()
		//{
		//	new Tuple<string, Tuple<int, int>> (Packager, new Tuple<int, int>(1, 1)),
		//	new Tuple<string, Tuple<int, int>> (Constructor, new Tuple<int, int>(4, 4)),
		//	new Tuple<string, Tuple<int, int>> (Smelter, new Tuple<int, int>(4, 4)),

		//	new Tuple<string, Tuple<int, int>> (Assembler, new Tuple<int, int>(4, 3)),
		//	new Tuple<string, Tuple<int, int>> (Blender, new Tuple<int, int>(4, 3)),
		//	new Tuple<string, Tuple<int, int>> (Foundry, new Tuple<int, int>(4, 3)),

		//	new Tuple<string, Tuple<int, int>> (Manufacturer, new Tuple<int, int>(4, 2)),
		//	new Tuple<string, Tuple<int, int>> (OilRefinery, new Tuple<int, int>(4, 2)),

		//	new Tuple<string, Tuple<int, int>> (ParticleAccelerator, new Tuple<int, int>(4, 1)),
		//};

		public static readonly List<string> BlackListedFactories = new()
		{
			"/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C",
			"/Script/FactoryGame.FGBuildGun",
		};
	}
}
