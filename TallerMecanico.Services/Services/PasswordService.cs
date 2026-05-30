using System.Security.Cryptography;
using System.Text;
using TallerMecanico.Services.Interfaces;

namespace TallerMecanico.Services.Services;

public class PasswordService : IPasswordService
{
    private const int Iterations = 100_000;
    private const int SaltSize = 16;
    private const int HashSize = 32;

    public string Hash(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256);
        var salt = algorithm.Salt;
        var hash = algorithm.GetBytes(HashSize);

        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Check(string hash, string password)
    {
        var parts = hash.Split('.', 3);
        if (parts.Length != 3)
            return false;

        if (!int.TryParse(parts[0], out var iterations))
            return false;

        var salt = Convert.FromBase64String(parts[1]);
        var storedHash = Convert.FromBase64String(parts[2]);

        using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var computedHash = algorithm.GetBytes(storedHash.Length);

        return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
    }
}
