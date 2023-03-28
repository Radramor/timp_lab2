using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using timp_lab2.Domain;

namespace timp_lab2.ViewModel;

public class AuthorizationViewModel
{
    private static List<List<object>>? _usersData;

    public AuthorizationViewModel()
    {
        _usersData = DeserializeUsersData.ParseData("Data/Authorize.txt");
    }

    public string? TryToEnter(string login, string password)
    {
        //throw new Exception(HashPassword(password));
        var userData =
            (_usersData ?? throw new InvalidOperationException()).FirstOrDefault(u => u[0].ToString() == login);
        if (userData == null)
            throw new ArgumentException(login);

        if (!VerifyPassword(userData[1].ToString(), password))
            throw new ArgumentException(login);

        return userData[2].ToString();
    }

    private string HashPassword(string password, byte[] salt = null!, bool needsOnlyHash = false)
    {
        if (salt is not { Length: 16 })
        {
            // generate a 128-bit salt using a secure PRNG
            salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            10000,
            256 / 8));

        return needsOnlyHash
            ? hashed
            :
            // password will be concatenated with salt using ':'
            $"{hashed}:{Convert.ToBase64String(salt)}";
    }

    private bool VerifyPassword(string? hashedPasswordWithSalt, string passwordToCheck)
    {
        // retrieve both salt and password from 'hashedPasswordWithSalt'
        var passwordAndHash = hashedPasswordWithSalt?.Split(':');
        if (passwordAndHash!.Length != 2)
            return false;
        var salt = Convert.FromBase64String(passwordAndHash[1]);
        // hash the given password
        var hashPasswordToCheck = HashPassword(passwordToCheck, salt, true);
        // compare both hashes
        return string.CompareOrdinal(passwordAndHash[0], hashPasswordToCheck) == 0;
    }
}