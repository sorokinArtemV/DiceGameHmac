namespace DiceGame.UserInteraction;

public sealed class Printer : IPrinter
{
    public void Print(string message) => Console.WriteLine(message);

    public void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void PrintDiceInputHelp(string message)
    {
        PrintError(message);
        Print("Example: dotnet run -- 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
    }
}