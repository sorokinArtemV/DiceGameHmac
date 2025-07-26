using DiceGame.DiceLogic;

namespace DiceGame.UserInteraction;

public sealed class DiceProbabilityTablePrinter
{
    private readonly DiceWinChanceEvaluator _evaluator;
    private readonly Printer _printer;

    public DiceProbabilityTablePrinter(DiceWinChanceEvaluator evaluator, Printer printer)
    {
        _evaluator = evaluator;
        _printer = printer;
    }

    public void ShowTable(List<Dice> diceList)
    {
        _printer.Print("\nProbability table (Dice A wins over Dice B):\n");

        string header = "Dice\\Dice";
        for (int i = 0; i < diceList.Count; i++)
            header += $" D{i}  ";
        
        _printer.Print(header);

        _printer.Print(new string('-', header.Length));

        for (int i = 0; i < diceList.Count; i++)
        {
            string row = $"D{i}       ";
            
            for (int j = 0; j < diceList.Count; j++)
            {
                string cell = i == j
                    ? " --- "
                    : $"{_evaluator.CalculateWinProbability(diceList[i], diceList[j]) * 100,3:F0}%";
                row += cell + " ";
            }

            _printer.Print(row.TrimEnd());
        }
    }
}