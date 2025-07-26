using System.Security.Cryptography;

namespace DiceGame.Randomization;

public sealed class HmacResult
{
    public int Value { get; }
    public byte[] Key { get; }
    public byte[] Hmac { get; }

    public HmacResult(int value, byte[] key, byte[] hmac)
    {
        Value = value;
        Key = key;
        Hmac = hmac;
    }

    public string KeyHex => BitConverter.ToString(Key).Replace("-", "");
    public string HmacHex => BitConverter.ToString(Hmac).Replace("-", "");
}

public class HmacGenerator
{
    public HmacResult Generate(int maxExclusive)
    {
        if (maxExclusive <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxExclusive), "Must be > 0");
        }

        int value = RandomNumberGenerator.GetInt32(0, maxExclusive);
        byte[] key = RandomNumberGenerator.GetBytes(32);
        byte[] message = BitConverter.GetBytes(value);

        using var hmac = new HMACSHA3_256(key);
        byte[] hash = hmac.ComputeHash(message);

        return new HmacResult(value, key, hash);
    }
}