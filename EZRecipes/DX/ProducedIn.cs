namespace EZRecipes.DX
{
	public static class ProducedIn
	{
		public static List<string>? From(List<Input.ProducedInContainer>? producedInContainer)
		{
			List<string> output = new();

			if (null != producedInContainer)
			{
				foreach (var produced in producedInContainer)
				{
					if (null != produced?.ProducedIn)
					{
						if (Constants.Recipe.NameResolver.TryGetValue(produced.ProducedIn, out string? producedIn))
						{
							output.Add(producedIn);
						}
						else
						{
							return null;
						}
					}
				}
			}

			return output;
		}
	}
}
