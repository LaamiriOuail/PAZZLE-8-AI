using Puzzle8;

internal class Program
{
    private static void Main(string[] args)
    {
        Puzzle Puzzle = new Puzzle();
        Puzzle.GetStateSpace();
        Puzzle.PrintPath();
    }
}