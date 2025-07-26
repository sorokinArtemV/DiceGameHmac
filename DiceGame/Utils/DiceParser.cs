using DiceGame.DiceLogic;

namespace DiceGame.Utils;

public sealed class DiceParser
{
    public List<Dice> Parse(string[] args)
    {
        var diceList = new List<Dice>();

        for (int i = 0; i < args.Length; i++)
        {
            var parts = args[i].Split(',');

            if (parts.Length != 6 || !parts.All(p => int.TryParse(p, out _)))
            {
                throw new ArgumentException($"Invalid dice format at index {i}. Example: 2,2,4,4,9,9");
            }

            IEnumerable<int> faces = parts.Select(int.Parse);
            diceList.Add(new Dice(faces));
        }

        return diceList;
    }
}