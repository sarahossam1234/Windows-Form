using Core.Framework.DataBase;
using System;

namespace FirstProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = GetUserInput();
            string encodedResult = EncodeToBase64(userInput);
            DisplayEncodedResult(encodedResult);
        }

        static string GetUserInput()
        {
            string userInput = null;

            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Enter a string to encode in Base-64:");
                userInput = Console.ReadLine(); 

               
                Console.WriteLine($"Raw user input: '{userInput}' (Length: {userInput?.Length})");

                userInput = userInput?.Trim(); 

               
                Console.WriteLine($"User input (trimmed): '{userInput}' (Length: {userInput?.Length})");

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Input cannot be null or empty. Please try again.");
                }
            }

            return userInput;
        }


        static string EncodeToBase64(string userInput)
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(userInput);
                return Convert.ToBase64String(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encoding to Base-64: {ex.Message}");
                return string.Empty; // Return empty string on error
            }
        }

        static void DisplayEncodedResult(string encodedResult)
        {
            if (!string.IsNullOrEmpty(encodedResult))
            {
                Console.WriteLine("\nEncoded Base-64 string: " + encodedResult);
            }
            else
            {
                Console.WriteLine("No valid encoded result to display.");
            }
        }
    }
}
