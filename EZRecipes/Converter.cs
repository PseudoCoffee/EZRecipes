using Newtonsoft.Json;

namespace EZRecipes
{
	public static class Converter
	{
		private static List<Input.RecipeGroup> GetRecipes(string filePath)
		{
			string json = File.ReadAllText(filePath);

			List<Input.RecipeGroup>? recipeGroups = JsonConvert.DeserializeObject<List<Input.RecipeGroup>>(json);

			return recipeGroups ?? new List<Input.RecipeGroup>();
		}

		private static IEnumerable<Output.Recipe> RecipeGroupsToRecipe(List<Input.RecipeGroup> recipeGroups)
		{
			foreach (Input.RecipeGroup recipeGroup in recipeGroups)
			{
				foreach (Output.Recipe recipe in DX.Recipe.From(recipeGroup.ClassName, recipeGroup.TechTier!.Value, recipeGroup.Recipes)
					.Where(r => null != r)
					.Select(r => r!))
				{
					yield return recipe!;
				}
			}
		}

		private static bool WriteRecipeToFile(string outputFolder, Output.Recipe recipe)
		{
			StreamWriter? writer = null;
			try
			{
				string text = JsonConvert.SerializeObject(
					recipe,
					Newtonsoft.Json.Formatting.Indented,
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
					});

				writer = new($"{outputFolder}\\{recipe.FileName}_EZ.json");
				writer.Write(text);
				writer.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{
				writer?.Close();
			}
		}

		private static void WriteAllToFile(string outputFolder, List<Output.Recipe> recipes)
		{
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
			List<Input.RecipeGroup>? recipeGroups = GetRecipes(inputPath);

			List<Output.Recipe> recipes = RecipeGroupsToRecipe(recipeGroups).ToList();

			WriteAllToFile(outputFolder, recipes);
		}
	}
}
