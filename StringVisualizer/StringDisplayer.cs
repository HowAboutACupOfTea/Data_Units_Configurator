//-----------------------------------------------------------------------
// <copyright file="StringDisplayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the StringDisplayer class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace StringVisualizer
{
    using System;

    /// <summary>
    /// This class implements functionality to display objects via the console.
    /// </summary>
    public class StringDisplayer : IDataVisualizerUnit<string>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: String displayer");
            Console.WriteLine("Description: Displays strings in text form.");
            Console.WriteLine("Input datatype: string[]");
            Console.WriteLine("Input description: The string/s to display.");
            Console.WriteLine("Output datatype: none");
            Console.WriteLine("Output description:");
        }

        /// <summary>
        /// This method displays string/s onto the console.
        /// </summary>
        /// <param name="data">The string/s to display.</param>
        public void DisplayData(string[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} must not be null.");
            }

            if (data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} must contain at least one string.");
            }

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
        }
    }
}