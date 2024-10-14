using Common.Constants.CI;
using Common.Output.Release_v1;

namespace EZRecipeV3
{
	public static class Converter
	{
		private static void FixRecipeValues(Recipe_v3 recipe, FileInfo fileInfo)
		{
			recipe.AssetPath = File.ReadLines(fileInfo.FullName).First().Replace("//", "");

			foreach (var ingredient in recipe.Ingredients)
			{
				if (!ingredient.Item.EndsWith("_C"))
				{
					ingredient.Item += "_C";
				}
			}
			foreach (var product in recipe.Products)
			{
				if (!product.Item.EndsWith("_C"))
				{
					product.Item += "_C";
				}
			}

			recipe.InternalName = fileInfo.Name.Replace(fileInfo.Extension, "");
		}

		private static Recipe_v3 ReadFile(FileInfo fileInfo)
		{
			string json = File.ReadAllText(fileInfo.FullName);

			Recipe_v3 recipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe_v3>(json)!;

			FixRecipeValues(recipe, fileInfo);

			return recipe;
		}

		public static IEnumerable<Recipe_v3> ReadFolder(string path)
		{
			DirectoryInfo directoryInfo = new(path);

			if (directoryInfo.Exists)
			{
				foreach (DirectoryInfo directoryInfos in directoryInfo.GetDirectories())
				{
					foreach (Recipe_v3 recipe in ReadFolder(directoryInfos.FullName))
					{
						yield return recipe;
					}
				}

				foreach (FileInfo fileInfos in directoryInfo.GetFiles())
				{
					if (fileInfos.Extension == ".json")
					{
						yield return ReadFile(fileInfos);
					}
				}
			}
		}

		private static IEnumerable<Recipe_v3> FilterRecipes(List<Recipe_v3> recipes, RecipeConfig recipeConfig)
		{
			foreach (var recipe in recipes)
			{
				Recipe_v3? outRecipe = RecipeModifier.Modify(recipe, recipeConfig);

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

		private static bool WriteRecipeToFile(string outputFolder, Recipe_v3 recipe)
		{
			StreamWriter? writer = null;

			string filePath = $"{outputFolder}\\{recipe.InternalName}";
			int? number = null;
			string ending = "_C_EZ.json";

			while (File.Exists(filePath + (number?.ToString() ?? "") + ending))
			{
				number = (number ?? -1) + 1;
			}

			filePath += (number?.ToString() ?? "") + ending;

			try
			{
				string text = $"//{recipe.AssetPath}{Environment.NewLine}" +
					Newtonsoft.Json.JsonConvert.SerializeObject(
						recipe,
						Newtonsoft.Json.Formatting.Indented,
						new Newtonsoft.Json.JsonSerializerSettings
						{
							NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
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

		private static void WriteAllToFile(string outputFolder, List<Recipe_v3> recipes, List<Recipe_v3> originalRecipes)
		{
			if (Directory.Exists(outputFolder))
			{
				Directory.Delete(outputFolder, true);
			}
			Directory.CreateDirectory(outputFolder);

			int recipesWritten = 0;
			foreach (var recipe in recipes)
			{
				if (WriteRecipeToFile(outputFolder, recipe))
				{
					recipesWritten++;
				}
			}

			Console.WriteLine($"Written {recipesWritten} out of {recipes.Count}({originalRecipes.Count}) recipes to file.");
		}

		public static void HandleFile(string inputPath, string outputFolder, string configFile)
		{
			List<Recipe_v3> recipes = ReadFolder(inputPath).ToList();

			RecipeConfig recipeConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<RecipeConfig>(File.ReadAllText(configFile))!;

			List<Recipe_v3> filteredRecipes = FilterRecipes(recipes, recipeConfig).ToList();

			WriteAllToFile(outputFolder, filteredRecipes, recipes);
		}

		private static void Stats(List<Recipe_v3> recipes)
		{
			HashSet<string> factories = new();
			HashSet<string> fluids = new();
			HashSet<string> slugs = new();

			foreach (var recipe in recipes)
			{
				if (recipe.ProducedIn != null)
				{
					foreach (var factory in recipe.ProducedIn)
					{
						factories.Add(factory);
					}
				}

				foreach (var item in recipe.Ingredients)
				{
					if (item.Amount > 500)
					{
						fluids.Add(item.Item);
					}
					if (item.Item.Contains("Crystal"))
					{
						slugs.Add(item.Item);
					}
				}
				foreach (var item in recipe.Products)
				{
					if (item.Amount > 500)
					{
						fluids.Add(item.Item);
					}
					if (item.Item.Contains("Crystal"))
					{
						slugs.Add(item.Item);
					}
				}
			}

			int placeholder = 100;
		}
	}
}
