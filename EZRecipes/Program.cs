namespace EZRecipes
{
	public class Program
	{
		public static readonly string inputFile = "[REDACTED]";
		public static readonly string outputFolder = "[REDACTED]";

		public static void Main(string[] args)
		{
			Converter.HandleFile(inputFile, outputFolder);
		}
	}
}