//-----------------------------------------------------------------------
// <copyright file="StringGenerator.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the StringGenerator class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace TextGenerator
{
    using System;

    /// <summary>
    /// This class implements functionality to generate data.
    /// </summary>
    public class StringGenerator : IDataSourceUnit<string>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: Text generator");
            Console.WriteLine("Description: Generates a word by using ten random letters from the alphabet.");
            Console.WriteLine("Input datatype: None");
            Console.WriteLine("Input description:");
            Console.WriteLine("Output datatype: string");
            Console.WriteLine("Output description: The generated word.");
        }

        /// <summary>
        /// This method generates text.
        /// </summary>
        /// <returns>The generated text.</returns>
        public string GenerateOutput()
        {
            Random r = new Random();

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            string output = "";

            for (int i = 0; i < 10; i++)
            {
                output += alphabet[r.Next(0, alphabet.Length)];
            }

            return output;
        }
    }
}

