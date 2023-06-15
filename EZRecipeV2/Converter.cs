using Newtonsoft.Json;

namespace EZRecipesV2
{
	public static class Converter
	{
		private static Common.Input.CI.Recipe ReadFile(string path)
		{
			string json = File.ReadAllText(path);

			return JsonConvert.DeserializeObject<Common.Input.CI.Recipe>(json)!;
		}

		private static IEnumerable<Common.Input.CI.Recipe> ReadFolder(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);

			if (directoryInfo.Exists)
			{
				foreach(var directoryInfos in directoryInfo.GetDirectories())
				{
					foreach(var recipe in ReadFolder(directoryInfos.FullName))
					{
						yield return recipe;
					}
				}

				foreach(var fileInfos in  directoryInfo.GetFiles())
				{
					yield return ReadFile(fileInfos.FullName);
				}
			}
		}

		private static IEnumerable<Common.Output.CI.Recipe> InRecipeToOutRecipe(List<Common.Input.CI.Recipe> recipes)
		{
			foreach(var recipe in recipes)
			{
				var outRecipe = Common.DX.CI.Recipe.From(recipe);

				if (outRecipe != null)
				{
					yield return outRecipe;
				}
				else
				{
					continue;
				}
			}
		}

		private static bool WriteRecipeToFile(string outputFolder, Common.Output.CI.Recipe recipe)
		{
			StreamWriter? writer = null;

			string filePath = $"{outputFolder}\\{recipe.FileName}";
			int? number = null;
			string ending = "_EZCI.json";

			while (File.Exists(filePath + (number?.ToString() ?? "") + ending))
			{
				number = (number ?? -1) + 1;
			}

			filePath += (number?.ToString() ?? "") + ending;

			try
			{
				string text = $"{recipe.ResourcePath}{Environment.NewLine}" +
					JsonConvert.SerializeObject(
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

		private static void WriteAllToFile(string outputFolder, List<Common.Output.CI.Recipe> recipes)
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
			List<Common.Input.CI.Recipe> inRecipes = ReadFolder(inputPath).ToList();

			List<Common.Output.CI.Recipe> outRecipes = InRecipeToOutRecipe(inRecipes).ToList();

			WriteAllToFile(outputFolder, outRecipes);
		}
	}
}
