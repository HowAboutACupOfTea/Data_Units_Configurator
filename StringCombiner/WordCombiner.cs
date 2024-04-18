//-----------------------------------------------------------------------
// <copyright file="WordCombiner.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the WordCombiner class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace StringCombiner
{
    using System;

    /// <summary>
    /// This class implements functionality to process data.
    /// </summary>
    public class WordCombiner : IDataProcessorUnit<string>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: Word combiner");
            Console.WriteLine("Description: Combines words.");
            Console.WriteLine("Input datatype: string[]");
            Console.WriteLine("Input description: The words to combine.");
            Console.WriteLine("Output datatype: string");
            Console.WriteLine("Output description: The combined words.");
        }

        /// <summary>
        /// This method combines the given words.
        /// </summary>
        /// <param name="data">The words to combine.</param>
        /// <returns>The combined words.</returns>
        public string ProcessData(string[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} must not be null.");
            }

            if (data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} must contain at least one word.");
            }

            string result = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                result += data[i];
            }

            return result;
        }
    }
}