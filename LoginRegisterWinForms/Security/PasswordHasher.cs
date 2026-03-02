using System;
using System.Security.Cryptography;

namespace LoginRegisterWinForms.Security
{
    public static class PasswordHasher
    {
        // PBKDF2 parameters
        private const int SaltSize = 16;   // 128-bit
        private const int KeySize = 32;    // 256-bit
        private const int Iterations = 100000;

        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256);

            byte[] key = pbkdf2.GetBytes(KeySize);

            // Store as: iterations.salt.key
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool Verify(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 3) return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);

            byte[] keyToCheck = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }
    }
}