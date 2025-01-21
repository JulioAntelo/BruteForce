namespace BruteForce
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    class Program
    {
        static void Main()
        {
            string[] passwords = File.ReadAllLines("C:\\Users\\wenas\\RiderProjects\\BruteForce\\BruteForce\\passwords.txt");
            Random random = new Random();
            string selectedPassword = passwords[random.Next(passwords.Length)];

            string hashedPassword = ComputeSHA256Hash(selectedPassword);
            Console.WriteLine($"Hashed password: {hashedPassword}");

            string crackedPassword = BruteForceAttack(passwords, hashedPassword);

            if (crackedPassword != null)
            {
                Console.WriteLine($"Password found: {crackedPassword}");
            }
            else
            {
                Console.WriteLine("Password not found.");
            }
        }

        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        static string BruteForceAttack(string[] dictionary, string targetHash)
        {
            string foundPassword = null;
            foreach (string password in dictionary)
            {
                if (ComputeSHA256Hash(password) == targetHash)
                {
                    foundPassword = password;
                    break;
                }
            }

            return foundPassword;
        }
    }
}

