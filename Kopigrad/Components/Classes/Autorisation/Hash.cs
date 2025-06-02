namespace Kopigrad.Components.Classes.Autorisation
{
    using System;
    using System.Security.Cryptography;

    public class Hash
    {

        public string Salt { get;  set; }


        public string HashValue { get;  set; }


        public void Create(string password)
        {
            // 1) Генерируем случайную соль 16 байт
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            Salt = Convert.ToBase64String(saltBytes);

            // 2) Вычисляем PBKDF2-хэш пароля с этой солью
            //    - 10000 итераций
            //    - длина выходного ключа: 32 байта (256 бит)
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                HashValue = Convert.ToBase64String(hashBytes);
            }
        }

        public bool Verify(string password)
        {
            if (string.IsNullOrEmpty(Salt) || string.IsNullOrEmpty(HashValue))
                throw new InvalidOperationException("Соль или хэш не инициализированы.");

            byte[] saltBytes = Convert.FromBase64String(Salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32);
                string computedHash = Convert.ToBase64String(hashBytes);
                return computedHash == HashValue;
            }
        }
    }

}
