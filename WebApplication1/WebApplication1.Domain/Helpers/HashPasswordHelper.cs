﻿using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Domain.Helpers;

public class HashPasswordHelper
{
    const int keySize = 64;
    const int iterations = 350000;
    static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
    
    public static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        return Convert.ToHexString(hash);
    }
}