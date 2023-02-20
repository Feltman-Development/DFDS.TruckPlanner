namespace DFDS.TP.Core.Utility;

public static class PasswordGenerator
{
    public static byte[] GenerateHash(string stringToHash)
    {
        using var rng = RandomNumberGenerator.Create();
        var secretKey = "yhQDsAxWCvNbnwx8tKp5Mz5ifYLQUytAidd7zZCmMJQXcto9bOAdQACNtwuBvV57"u8.ToArray();
        using var hmac = new HMACSHA256(secretKey);
        return hmac.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));
    }
}