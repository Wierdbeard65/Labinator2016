//-----------------------------------------------------------------------
// <copyright file="PasswordHash.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: havoc AT defuse.ca
/// www: http://crackstation.net/hashing-security.htm
/// Compatibility: .NET 3.0 and later.
/// Version: 1.0 - Initial build.
/// </summary>
namespace Labinator2016.Lib.Utilities
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// </summary>
    public class PasswordHash
    {
        // The following constants may be changed without breaking existing hashes.

        /// <summary>
        /// The salt byte size
        /// </summary>
        public const int SALTBYTESIZE = 24;

        /// <summary>
        /// The hash byte size
        /// </summary>
        public const int HASHBYTESIZE = 24;

        /// <summary>
        /// The PBKD f2 iterations
        /// </summary>
        public const int PBKDF2ITERATIONS = 1000;

        /// <summary>
        /// The iteration index
        /// </summary>
        public const int ITERATIONINDEX = 0;

        /// <summary>
        /// The salt index
        /// </summary>
        public const int SALTINDEX = 1;

        /// <summary>
        /// The PBKD f2 index
        /// </summary>
        public const int PBKDF2INDEX = 2;

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password)
        {
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALTBYTESIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, PBKDF2ITERATIONS, HASHBYTESIZE);
            return PBKDF2ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            string[] split = correctHash.Split(delimiter);
            int iterations = int.Parse(split[ITERATIONINDEX]);
            byte[] salt = Convert.FromBase64String(split[SALTINDEX]);
            byte[] hash = Convert.FromBase64String(split[PBKDF2INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}