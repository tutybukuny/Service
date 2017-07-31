using System;
using System.Linq;

namespace BusinessTier.Core
{
    public class TokenGen
    {
        public static string AutoGenerate()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }
    }
}