﻿namespace Common.Constants.FRM
{
	public static class Recipe
	{
		public static readonly List<string> Fluids = new()
		{
			"Desc_Water_C",
			"Desc_LiquidOil_C",
			"Desc_HeavyOilResidue_C",
			"Desc_LiquidFuel_C",
			"Desc_LiquidTurboFuel_C",
			"Desc_LiquidBiofuel_C",
			"Desc_AluminaSolution_C",
			"Desc_SulfuricAcid_C",
			"Desc_NitrogenGas_C",
			"Desc_NitricAcid_C"
		};

		public static readonly Dictionary<string, string> NameResolver = new()
		{
			{ "Workbench", "Workbench" },
			{ "Smelter", "Build_SmelterMk1" },
			{ "Constructor", "Build_ConstructorMk1"},
			{ "Assembler", "Build_AssemblerMk1"},
			{ "Foundry", "Build_FoundryMk1"},
			{ "Manufacturer", "Build_ManufacturerMk1"},
			{ "Refinery", "Build_OilRefinery"},
			{ "Packager", "Build_Packager"},
			{ "Blender", "Build_Blender"},
			{ "Particle Accelerator", "Build_HadronCollider"}
		};
	}
}
