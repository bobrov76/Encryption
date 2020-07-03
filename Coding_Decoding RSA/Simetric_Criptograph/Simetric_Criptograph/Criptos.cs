﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Simetric_Criptograph
{
    class Criptos
    {
        //Конвертация полученного текста и  пароля
        public string EncryptText(string sms)
        {            
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(sms);
            byte[] passwordBytes = Encoding.UTF8.GetBytes("TestDecoding");            
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);// хэширование пароля SHA256
            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }

        //метод шифрования строки
        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };//установка соли

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

    }
}
