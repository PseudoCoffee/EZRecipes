using Microsoft.VisualBasic;

namespace Common.DX.CI
{
	public static class Recipe
	{
		public static Output.CI.Recipe? From(Input.CI.Recipe recipe)
		{
			IEnumerable<Output.CI.Ingredient>? ingredients = null != recipe.JS_LibValue.mIngredients ? Ingredient.From(recipe.JS_LibValue.mIngredients) : null;
			IEnumerable<Output.CI.Product>? products = null != recipe.JS_LibValue.mProduct ? Product.From(recipe.JS_LibValue.mProduct) : null;

			if (null != ingredients && null != products)
			{
				return new Output.CI.Recipe
				{
					ResourcePath = $"/{recipe.JS_LibOuter.Replace("Default__", "")}",
					Name = $"EZ_{recipe.JS_LibValue.mDisplayName}2",
					FileName = recipe.JS_LibOuter[recipe.JS_LibOuter.LastIndexOf(".")..].Replace(".Default__", ""),
					Ingredients = ingredients.ToList(),
					Products = products.ToList(),
					ManufacturingDuration = recipe.JS_LibValue?.mManufactoringDuration,
					ManualManufacturingMultiplier = recipe.JS_LibValue?.mManualManufacturingMultiplier
				};
			}
			else
			{
				return null;
			}
		}
	}
}
