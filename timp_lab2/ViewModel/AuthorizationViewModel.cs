using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using timp_lab2.Domain;

namespace timp_lab2.ViewModel;

public class AuthorizationViewModel
{
    private static List<List<object>> _usersData;

    public AuthorizationViewModel()
    {
        _usersData = DeserializeUsersData.ParseData("Data/Authorize.txt");
    }

    public string? TryToEnter(string login, string password)
    {
        //throw new Exception(HashPassword(password));
        var userData = _usersData.FirstOrDefault(u => u[0].ToString() == login);
        if (userData == null) 
            throw new ArgumentException(login);
        
        if (!VerifyPassword(userData[1].ToString(), password)) 
            throw new ArgumentException(login);
        
        return userData[2].ToString();
    }

    private string HashPassword(string password, byte[] salt = null, bool needsOnlyHash = false)
    {
        if (salt is not { Length: 16 })
        {
            // generate a 128-bit salt using a secure PRNG
            salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return needsOnlyHash ? hashed :
            // password will be concatenated with salt using ':'
            $"{hashed}:{Convert.ToBase64String(salt)}";
    }

    private bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
    {
        // retrieve both salt and password from 'hashedPasswordWithSalt'
        var passwordAndHash = hashedPasswordWithSalt.Split(':');
        if (passwordAndHash.Length != 2)
            return false;
        var salt = Convert.FromBase64String(passwordAndHash[1]);
        // hash the given password
        var hashOfpasswordToCheck = HashPassword(passwordToCheck, salt, true);
        // compare both hashes
        if (String.Compare(passwordAndHash[0], hashOfpasswordToCheck) == 0)
        {
            return true;
        }
        return false;
    }
}