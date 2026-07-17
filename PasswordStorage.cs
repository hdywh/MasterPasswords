using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    public static class PasswordStorage
    {
        public static string FilePath = "passwords.json";
        public static List<PasswordEntry> Passwords = new List<PasswordEntry>();

        private static string MasterHashFile = "master.hash";

        public static void Save()
        {
            File.WriteAllText(FilePath,
                JsonConvert.SerializeObject(Passwords, Formatting.Indented));
        }

        public static void Load(string path)
        {
            FilePath = path;
            Passwords = JsonConvert.DeserializeObject<List<PasswordEntry>>(
                File.ReadAllText(path));
        }

        public static bool CheckMasterPassword(string input)
        {
            if (!File.Exists(MasterHashFile))
            {
                File.WriteAllText(MasterHashFile, Hash(input));
                return true;
            }

            return File.ReadAllText(MasterHashFile) == Hash(input);
        }

        private static string Hash(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                return System.Convert.ToBase64String(bytes);
            }
        }
    }
}