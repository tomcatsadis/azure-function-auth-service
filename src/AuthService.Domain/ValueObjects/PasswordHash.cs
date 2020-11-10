using System;
using System.Security.Cryptography;

/*
 * password hashing reference: https://stackoverflow.com/a/10402129/251311
 * */

namespace AuthService.Domain.ValueObjects
{
    public sealed class PasswordHash
    {
        private string _text;

        public PasswordHash(string text)
        {
            _text = text;
        }

        public static PasswordHash Hash(string password)
        {
            /* Create the salt value with a cryptographic PRNG */
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            /* Create the Rfc2898DeriveBytes and get the hash value: */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            /* Combine the salt and password bytes for later use */
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            /* return the combined salt + hash into a string */
            return new PasswordHash(Convert.ToBase64String(hashBytes));
        }

        public bool Verify(Password password)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(_text);

            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator PasswordHash(string text)
        {
            return new PasswordHash(text);
        }

        public static implicit operator string(PasswordHash name)
        {
            return name._text;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is PasswordHash)
            {
                return ((PasswordHash)obj)._text == _text;
            }
            else if (obj is string)
            {
                return obj.ToString() == _text;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}
