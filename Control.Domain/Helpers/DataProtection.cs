using Control.Domain.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Security.Cryptography;

namespace Control.Domain.Helpers
{
    public class DataProtection
    {

        public readonly IDataProtector dataProtector;
        public DataProtection(
            IDataProtectionProvider dataProtectionProvider
            )
        {
            dataProtector = dataProtectionProvider.CreateProtector("Key_Secret_Aleatorio_Seguro");
        }
        public string EncryptData(string dato)
        {
            return dataProtector.Protect(dato);
        }

        public string DecryptData(string dato)
        {
            return dataProtector.Unprotect(dato);
        }

        public static PasswordHash Hash(string textoPlano)
        {
            var sal = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }

            return Hash(textoPlano, sal);
        }

        private static PasswordHash Hash(string textoPlano, byte[] sal)
        {
            var llaveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
                salt: sal, prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            var hash = Convert.ToBase64String(llaveDerivada);

            return new PasswordHash()
            {
                Hash = hash,
                Sal = sal
            };
        }
    }
}
