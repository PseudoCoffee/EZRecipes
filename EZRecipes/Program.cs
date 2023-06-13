namespace EZRecipes
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length > 1)
			{
				Converter.HandleFile(args[0], args[1]);
			}
			else
			{
				Console.WriteLine("Not enough arguments.");
			}
		}
	}
}