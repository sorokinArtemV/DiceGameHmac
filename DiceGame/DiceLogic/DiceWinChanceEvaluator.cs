namespace DiceGame.DiceLogic;

public sealed class DiceWinChanceEvaluator
{
    public double CalculateWinProbability(Dice a, Dice b)
    {
        int wins = 0, total = 0;

        foreach (var faceA in a.Faces)
        {
            foreach (var faceB in b.Faces)
            {
                if (faceA > faceB)
                {
                    wins++;
                }

                total++;
            }
        }

        return total == 0 ? 0.0 : (double)wins / total;
    }
}