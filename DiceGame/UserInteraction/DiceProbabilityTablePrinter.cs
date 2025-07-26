using ConsoleTableExt;
using DiceGame.DiceLogic;
using System.Globalization;

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
        _printer.Print("\nProbability of the win for the user:\n");

        var tableData = new List<List<object>>();

        for (int userIdx = 0; userIdx < diceList.Count; userIdx++)
        {
            List<object> row = [diceList[userIdx].ToString()];
            
            row.AddRange(diceList.Select((t, compIdx) => userIdx == compIdx
                    ? 1.0 / 3.0
                    : _evaluator.CalculateWinProbability(diceList[userIdx], t))
                .Select(p => $"{p:F4}"));

            tableData.Add(row);
        }

        List<string> headers = new List<string> { "User dice v" };
        
        headers.AddRange(diceList.Select(d => d.ToString()));

        ConsoleTableBuilder
            .From(tableData)
            .WithColumn(headers)
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine();
    }
}