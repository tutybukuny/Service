using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessTier.Core
{
    public class TokenGen
    {
        private const string String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        private static readonly Random Rand = new Random();

        public static string AutoGenerate()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            var add = String[Rand.Next(String.Length)] + "";
            token = Regex.Replace(token, @"\W", add);

            return token;
        }
    }
}