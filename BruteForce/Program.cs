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

        // Funcion para calcular el hash SHA256 de una cadena de texto
        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la cadena de texto en un array de bytes y calcula su hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Convierte el array de bytes en una cadena hexadecimal y la devuelve
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        // Funcion para realizar un ataque de fuerza bruta
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