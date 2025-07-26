using System.Security.Cryptography;
using DiceGame.DiceLogic;
using DiceGame.Randomization;
using DiceGame.UserInteraction;

namespace DiceGame.Engine;

public sealed class DiceGameEngine
{
    private readonly List<Dice> _diceList;
    private readonly FairDiceRoller _diceRoller;
    private readonly Printer _printer;
    private readonly DiceProbabilityTablePrinter _tableGenerator;
    private readonly IInputReader _input;

    public DiceGameEngine(
        List<Dice> diceList,
        FairDiceRoller diceRoller,
        Printer printer,
        DiceProbabilityTablePrinter tableGenerator,
        IInputReader input)
    {
        _diceList = diceList;
        _diceRoller = diceRoller;
        _printer = printer;
        _tableGenerator = tableGenerator;
        _input = input;
    }

    public void Start()
    {
        _printer.Print("Let's determine who makes the first move.");

        var coinResult = _diceRoller.ExecuteFairRoll(2, _input, _printer);
        bool userGoesFirst = coinResult == 0;

        int userIndex, compIndex;

        if (userGoesFirst)
        {
            _printer.PrintSuccess("You make the first move.");

            userIndex = PromptDiceSelection(null);
            compIndex = SelectComputerDice(userIndex);
        }
        else
        {
            _printer.PrintSuccess("I make the first move.");
            compIndex = SelectComputerDice(null);

            _printer.Print($"I choose the {_diceList[compIndex]} dice.");
            userIndex = PromptDiceSelection(compIndex);
        }

        Dice userDice = _diceList[userIndex];
        Dice compDice = _diceList[compIndex];

        _printer.Print("\nIt's time for my roll.");

        int compFinal = _diceRoller.ExecuteFairRoll(compDice.Faces.Count, _input, _printer);
        int compResult = compDice.Faces[compFinal];

        _printer.Print($"My roll result is {compResult}.");

        _printer.Print("\nIt's time for your roll.");

        int userFinal = _diceRoller.ExecuteFairRoll(userDice.Faces.Count, _input, _printer);
        int userResult = userDice.Faces[userFinal];

        _printer.Print($"Your roll result is {userResult}.");

        if (userResult > compResult)
        {
            _printer.PrintSuccess("You win!");
        }
        else if (userResult < compResult)
        {
            _printer.PrintError("I win!");
        }
        else
        {
            _printer.PrintWarning("It's a draw!");
        }
    }

    private int PromptDiceSelection(int? excluded)
    {
        while (true)
        {
            for (int i = 0; i < _diceList.Count; i++)
            {
                if (excluded.HasValue && i == excluded.Value)
                {
                    continue;
                }

                _printer.Print($"{i} - {_diceList[i]}");
            }

            _printer.Print("X - exit\n? - help");

            string? input = _input.ReadLine();

            if (input?.ToUpper() == "X")
                Environment.Exit(0);

            if (input == "?")
            {
                _tableGenerator.ShowTable(_diceList);
                continue;
            }

            if (int.TryParse(input, out int idx) &&
                idx >= 0 && idx < _diceList.Count &&
                (!excluded.HasValue || idx != excluded.Value))
            {
                _printer.Print($"You choose the {_diceList[idx]} dice.");
                
                return idx;
            }

            _printer.PrintError("Invalid selection.");
        }
    }

    private int SelectComputerDice(int? excluded)
    {
        int index;

        do
        {
            index = RandomNumberGenerator.GetInt32(0, _diceList.Count);
        } while (excluded.HasValue && index == excluded.Value);

        return index;
    }
}