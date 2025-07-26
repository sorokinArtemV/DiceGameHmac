namespace DiceGame.UserInteraction;

public interface IPrinter
{
    void Print(string message);
    void PrintError(string message);
    void PrintSuccess(string message);
    void PrintWarning(string message);
    void PrintDiceInputHelp(string message);
}