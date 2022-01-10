using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common
{
    public class HMACSHA512Hasher : IHasher
    {
        private readonly string encryptionKey;

        public HMACSHA512Hasher(string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException("encryptionKey");
            }

            this.encryptionKey = encryptionKey;
        }

        public string Hash(string message)
        {
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(this.encryptionKey);
            var messageBytes = encoding.GetBytes(message);

            var cryptoProvider = new HMACSHA512(keyByte);

            var result = cryptoProvider.ComputeHash(messageBytes);
            var hash = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                hash.Append(result[i].ToString("X2"));
            }

            return hash.ToString();
        }

        public string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }

    public interface IHasher
    {
        string Hash(string message);
        string GetUniqueKey(int maxSize);
    }

}
