namespace DiceGame.DiceLogic;

public sealed class Dice
{
    public List<int> Faces { get; }

    public Dice(IEnumerable<int> faces)
    {
        var list = faces.ToList();

        if (list.Count == 0)
        {
            throw new ArgumentException("A dice must have at least one face.");
        }

        Faces = list;
    }

    public override string ToString()
    {
        return $"[{string.Join(",", Faces)}]";
    }
}