using DiceGame.UserInteraction;

namespace DiceGame.Randomization;

public class FairDiceRoller
{
    private readonly HmacGenerator _generator;

    public FairDiceRoller(HmacGenerator generator)
    {
        _generator = generator;
    }

    public int ExecuteFairRoll(int maxExclusive, IInputReader input, Printer printer)
    {
        var hmacResult = _generator.Generate(maxExclusive);
        printer.Print($"I selected a random value in the range 0..{maxExclusive - 1} (HMAC={hmacResult.HmacHex})");

        int userValue = PromptUserInput(maxExclusive, input, printer);
        int final = (userValue + hmacResult.Value) % maxExclusive;

        printer.Print($"My number is {hmacResult.Value} (KEY={hmacResult.KeyHex})");
        printer.Print(
            $"The fair number generation result is {hmacResult.Value} + {userValue} = {final} (mod {maxExclusive})");

        return final;
    }

    private int PromptUserInput(int maxExclusive, IInputReader input, Printer printer)
    {
        while (true)
        {
            printer.Print($"Add your number modulo {maxExclusive}:");
            for (int i = 0; i < maxExclusive; i++)
                printer.Print($"{i} - {i}");

            printer.Print("X - exit\n? - help");
            var raw = input.ReadLine();

            if (raw?.Trim().ToUpper() == "X") Environment.Exit(0);
            if (raw == "?")
            {
                printer.Print($"Enter a number between 0 and {maxExclusive - 1}.");
                continue;
            }

            if (int.TryParse(raw, out int num) && num >= 0 && num < maxExclusive)
                return num;

            printer.PrintError("Invalid input. Try again.");
        }
    }
}