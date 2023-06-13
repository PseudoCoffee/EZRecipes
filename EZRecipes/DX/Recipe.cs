namespace EZRecipes.DX
{
	public static class Recipe
	{
		public static Output.Recipe? From(string unlockedBy, int techTier, Input.Recipe recipe)
		{
			List<string>? producedIn = DX.ProducedIn.From(recipe.ProducedIn);

			if (null != producedIn)
			{
				bool isPackager = producedIn!.Contains("Build_Packager");

				float durationModifier = 2.0f;
				int duration = (int)(durationModifier * ((techTier + 1 / 2) + 1));
				int ingredientCount = 24;
				int productCount = isPackager ? 24 : 12;

				List<Output.Ingredient> ingredients = DX.Ingredient.From(recipe.Ingredients, ingredientCount).ToList();
				List<Output.Product> products = DX.Product.From(recipe.Products, productCount).ToList();
				
				return new Output.Recipe
				{
					Name = recipe.RecipeName,
					FileName = recipe.ClassName ?? throw new ArgumentNullException(nameof(recipe.ClassName)),
					Ingredients = ingredients,
					Products = products,
					ProducedIn = producedIn,
					OverrideCategory = recipe.Category,
					ManufacturingDuration = duration,
					ManualManufacturingMultiplier = recipe.ManualDuration / recipe.ManufacturingDuration,
					UnlockedBy = new List<string> { unlockedBy },
					VariablePowerConsumptionFactor = null,
					VariablePowerConsumptionConstant = null,
					ManufacturingMenuPriority = null,
					OverrideName = false,
					ClearIngredients = ingredients.Any(),
					ClearProducts = products.Any(),
					ClearBuilders = producedIn.Any(),
				};
			}
			else
			{
				return null;
			}
		}

		public static IEnumerable<Output.Recipe?> From(string unlockedBy, int techTier, List<Input.Recipe> recipes)
		{
			foreach (var recipe in recipes)
			{
				yield return From(unlockedBy, techTier, recipe);
			}
		}
	}
}
