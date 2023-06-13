using Newtonsoft.Json;

namespace EZRecipes
{
	public static class Converter
	{
		private static List<Input.RecipeGroup>? GetRecipes(string filePath)
		{
			string json = File.ReadAllText(filePath);

			List<Input.RecipeGroup>? recipeGroups = JsonConvert.DeserializeObject<List<Input.RecipeGroup>>(json);

			return recipeGroups;
		}

		private static List<Output.Recipe?> Convert(Input.RecipeGroup recipeGroup)
		{
			return DX.Recipe.From(recipeGroup.ClassName, recipeGroup.TechTier!.Value, recipeGroup.Recipes).ToList();
		}

		private static void WriteRecipeToFile(string outputFolder, Output.Recipe recipe)
		{
			string text = JsonConvert.SerializeObject(
				recipe,
				Newtonsoft.Json.Formatting.Indented,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});

			using StreamWriter writer = new StreamWriter($"{outputFolder}\\{recipe.FileName}_EZ.json");
			writer.Write(text);
		}

		private static void WriteAllToFile(string outputFolder, List<Output.Recipe> recipes)
		{
			foreach (var recipe in recipes)
			{
				WriteRecipeToFile(outputFolder, recipe);
			}
		}

		public static void HandleFile(string inputPath, string outputFolder)
		{
			List<Input.RecipeGroup>? recipeGroups = GetRecipes(inputPath);

			List<Output.Recipe> recipes = new();

			if (null != recipeGroups)
			{
				foreach (var groups in recipeGroups)
				{
					recipes.AddRange(Convert(groups).Where(item => null != item)!);
				}
			}

			WriteAllToFile(outputFolder, recipes);
		}
	}
}
