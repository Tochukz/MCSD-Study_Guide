using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;
using System.Security.Permissions;
using System.IO;

namespace Chapter13
{
    class Program
    {
        enum HashEnum{ SH1, SH2, SH3, SH5, MD1};
        private static string privateKey;
        static void Main(string[] args)
        {
            /*
            //SymmetricEncryption
            string message = "We meet tomorrow";
            byte[] encryptedByes = SymmetricEncryption(message);
            string str = Encoding.UTF8.GetString(encryptedByes);
            Console.WriteLine(str);

            //SymmetricDecryption
            string decrypMessage = SymmetricDecryption(encryptedByes);
            Console.WriteLine(decrypMessage);

            //AsymmetricEncryption
            byte[] asymEncBytes = AsymmetricEncryption(message);
            string asymEncStr = Encoding.UTF8.GetString(asymEncBytes);
            Console.WriteLine(asymEncStr);

            //AsymmetricDecryption
            string aysmDecryptedMsg = AsymmetricDecryption(asymEncBytes);
            Console.WriteLine($"Asym Decrypted Msg: {aysmDecryptedMsg }");

            //Generate Kay and IV in Symmetric algorithm
            SymmetricAlgorithm symAlgo = SymmetricAlgorithm.Create();
            symAlgo.GenerateIV();
            symAlgo.GenerateKey();
            */

            /*Hashing a string*/
            string sh1Str = HashString("tochukwu", HashEnum.SH1);
            string sh5Str = HashString("tochukwu", HashEnum.SH5);
            string md160 = HashString("tochukwu");
            Console.WriteLine($"Hashed tochukwu = {sh1Str}");
            Console.WriteLine($"Hashed tochukwu = {sh5Str}");
            Console.WriteLine($"Hashed tochukwu = {md160}");

            /*Salted Hash*/
            string pass = "tochukwu";
            string saltedPassHash = HashWithSalt(pass);
            Console.WriteLine($"Salted pass Hash: {saltedPassHash}");

            //Console.WriteLine(HashEnum.SH1);
            Console.ReadLine();
        }
        static byte[] SymmetricEncryption(string message)
        {       
            //Encode string as Bytes
            byte[] messgeByte = Encoding.UTF8.GetBytes(message);

            //Create SymmetricAlgorithm object
            SymmetricAlgorithm symmAlgo = SymmetricAlgorithm.Create();
            
            //Create encryptor using the SymmetricAlgorithm object
            ICryptoTransform encryptor = symmAlgo.CreateEncryptor(symmAlgo.Key, symmAlgo.IV);

            //Transform Byte encoding message to encryted byte
            byte[] encryptedByte = encryptor.TransformFinalBlock(messgeByte, 0, messgeByte.Length);
           

            //Encoding encypted byte as string for delivery
            //string encryptedStr = Encoding.UTF8.GetString(encryptedByte);

            return encryptedByte;
        }
        static string SymmetricDecryption(byte[] encryptedByte)
        {         

            SymmetricAlgorithm symmAlgo = SymmetricAlgorithm.Create();
            ICryptoTransform decryptor = symmAlgo.CreateDecryptor(symmAlgo.Key, symmAlgo.IV);

            //byte[] decryptedByte = decryptor.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);


            //string message = Encoding.UTF8.GetString(decryptedByte);

            //return message;      

            //TODO

            return "NotWorkingYetException";
        }
        static byte[] AsymmetricEncryption(string message)
        {
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            RSAParameters RSAKeyInfo = rsa.ExportParameters(false);

            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            Program.privateKey = privateKey;

            //setting publicKey to be used in the encryption
            rsa.FromXmlString(publicKey);

            byte[] encryptedByte = rsa.Encrypt(messageByte, true);
            return encryptedByte;
        }
        static string AsymmetricDecryption(byte[] encBytes)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(Program.privateKey);

            byte[] decryptedBytes = rsa.Decrypt(encBytes, true);
            string message = Encoding.UTF8.GetString(decryptedBytes);

            return message;
        }
        static void EncryptStream(string message)
        {
            SymmetricAlgorithm symmAlgo = SymmetricAlgorithm.Create();
            ICryptoTransform encryptor = symmAlgo.CreateEncryptor(symmAlgo.Key, symmAlgo.IV);

            MemoryStream mStream = new MemoryStream();

            CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);

            using(StreamWriter writer = new StreamWriter(cStream))
            {
                writer.Write(message);
            }

            mStream.Close();
            cStream.Close();
            //TODO
        }
        static void DecryptStream(byte[] encryptedBytes)
        {
            //TODO
        }
        static byte[] EncryptWithProtected(string message)
        {
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            //byte[] encrytpedBytes = ProtectedData.Protect(messageByte, null, DataProtectionScope.CurrentUser);
            
            return new byte[8];
            //TODO
        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        static void DelarativePermission()
        {

            //Permission granted 
            //Permission was requested using the declarative method
        }
        static void ImperativePermission()
        {
            //Asking permission using Imperative method
            FileIOPermission permission = new FileIOPermission(PermissionState.None);
            permission.AllLocalFiles = FileIOPermissionAccess.Read;
            permission.Demand();
        }
        static string HashString(string password, HashEnum? hash = null)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            HashAlgorithm hashAlgo;
            switch (hash)
            {
                case HashEnum.SH1:
                    HashAlgorithm sha1 = SHA1.Create();
                    hashAlgo = sha1;
                    break;
                case HashEnum.SH2:
                    HashAlgorithm sha256 = SHA256.Create();
                    hashAlgo = sha256;
                    break;
                case HashEnum.SH3:
                    HashAlgorithm sha384 = SHA384.Create();
                    hashAlgo = sha384;
                    break;
                case HashEnum.SH5:
                    HashAlgorithm sha512 = SHA512.Create();
                    hashAlgo = sha512;
                    break;
                default:
                    HashAlgorithm md160 = RIPEMD160.Create();
                    hashAlgo = md160;
                    Console.WriteLine("Hashed with RIPEMD160");
                    break;
            }
            
            byte[] hashBytes = hashAlgo.ComputeHash(passwordBytes);


            StringBuilder builder = new StringBuilder();
            foreach(byte b in hashBytes)
            {
                builder.Append(b);
            }

            return builder.ToString();
        }

        static string HashWithSalt(string password)
        {
            Guid guid = new Guid();
            string saltedPass = password + guid;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPass);
            
            HashAlgorithm algo = SHA384.Create();
            byte[] hashedBytes  = algo.ComputeHash(passwordBytes);

            StringBuilder builder = new StringBuilder();
            foreach(byte b in hashedBytes)
            {
                builder.Append(b);
            }

            return builder.ToString();


        }
        static bool ValidateSaltedHash(string name, string hash)
        {
            Guid guid = new Guid();
            guid.GetHashCode();

            //TODO
            return false;
        }
    }
}
