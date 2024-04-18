//-----------------------------------------------------------------------
// <copyright file="StringGenerator.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the StringGenerator class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace WordGenerator
{
    using System;

    /// <summary>
    /// This class implements functionality to generate data.
    /// </summary>
    public class StringGenerator : IDataSourceUnit<string>
    {
        /// <summary>
        /// The Random instance of the class.
        /// </summary>
        public readonly Random Random = new Random();

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
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            string output = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                output += alphabet[this.Random.Next(0, alphabet.Length)];
            }

            return output;
        }
    }
}