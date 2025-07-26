using DiceGame.DiceLogic;
using DiceGame.Engine;
using DiceGame.Randomization;
using DiceGame.UserInteraction;
using DiceGame.Utils;

var printer = new Printer();

if (args.Length < 3)
{
    printer.PrintDiceInputHelp("Not enough arguments.");
    return;
}

try
{
    DiceParser parser = new();
    List<Dice> diceList = parser.Parse(args);

    var game = new DiceGameEngine(
        diceList,
        new FairDiceRoller(new HmacGenerator()),
        printer,
        new DiceProbabilityTablePrinter(new DiceWinChanceEvaluator(), printer),
        new ConsoleInput()
    );

    game.Start();
}
catch (ArgumentException ex)
{
    printer.PrintDiceInputHelp($"Input error: {ex.Message}");
}