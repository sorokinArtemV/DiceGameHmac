namespace DiceGame.UserInteraction;

public sealed class ConsoleInput : IInputReader
{
    public string? ReadLine() => Console.ReadLine();
}