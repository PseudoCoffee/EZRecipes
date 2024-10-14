namespace EZRecipeV3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length > 2)
			{
				Converter.HandleFile(args[0], args[1], args[2]);
			}
			else
			{
				Console.WriteLine("Not enough arguments.");
			}
		}
	}
}