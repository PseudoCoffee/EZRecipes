using Common.DX.FRM;
using Newtonsoft.Json;

namespace EZRecipes
{
    public static class Converter
	{
		private static List<Common.Input.FRM.RecipeGroup> GetRecipes(string filePath)
		{
			string json = File.ReadAllText(filePath);

			List<Common.Input.FRM.RecipeGroup>? recipeGroups = JsonConvert.DeserializeObject<List<Common.Input.FRM.RecipeGroup>>(json);

			return recipeGroups ?? new List<Common.Input.FRM.RecipeGroup>();
		}

		private static IEnumerable<Common.Output.FRM.Recipe> RecipeGroupsToRecipe(List<Common.Input.FRM.RecipeGroup> recipeGroups)
		{
			foreach (Common.Input.FRM.RecipeGroup recipeGroup in recipeGroups)
			{
				foreach (Common.Output.FRM.Recipe recipe in Recipe.From(recipeGroup.ClassName, recipeGroup.TechTier!.Value, recipeGroup.Recipes)
					.Where(r => null != r)
					.Select(r => r!))
				{
					yield return recipe!;
				}
			}
		}

		private static bool WriteRecipeToFile(string outputFolder, Common.Output.FRM.Recipe recipe)
		{
			StreamWriter? writer = null;

			string filePath = $"{outputFolder}\\{recipe.FileName}";
			int? number = null;
			string ending = "_EZFRM.json";

			while (File.Exists(filePath + (number?.ToString() ?? "") + ending))
			{
				number = (number ?? -1) + 1;
			}

			filePath += (number?.ToString() ?? "") + ending;

			try
			{
				string text = JsonConvert.SerializeObject(
					recipe,
					Newtonsoft.Json.Formatting.Indented,
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
					});

				writer = new(filePath);
				writer.Write(text);
				writer.Close();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				writer?.Close();
			}
		}

		private static void WriteAllToFile(string outputFolder, List<Common.Output.FRM.Recipe> recipes)
		{
			Directory.Delete(outputFolder, true);
			Directory.CreateDirectory(outputFolder);

			int recipesWritten = 0;
			foreach (var recipe in recipes)
			{
				if (WriteRecipeToFile(outputFolder, recipe))
				{
					recipesWritten++;
				}
			}
			Console.WriteLine($"Written {recipesWritten} out of {recipes.Count} recipes to file.");
		}

		public static void HandleFile(string inputPath, string outputFolder)
		{
			List<Common.Input.FRM.RecipeGroup>? recipeGroups = GetRecipes(inputPath);

			List<Common.Output.FRM.Recipe> recipes = RecipeGroupsToRecipe(recipeGroups).ToList();

			WriteAllToFile(outputFolder, recipes);
		}
	}
}
