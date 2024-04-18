//-----------------------------------------------------------------------
// <copyright file="NumberDisplayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the NumberDisplayer class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace NumberVisualizer
{
    using System;

    /// <summary>
    /// This class implements functionality to display data via the console.
    /// </summary>
    public class NumberDisplayer : IDataVisualizerUnit<int>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: Number displayer");
            Console.WriteLine("Description: Displays numbers.");
            Console.WriteLine("Input datatype: int32[]");
            Console.WriteLine("Input description: The number/s to display.");
            Console.WriteLine("Output datatype: none");
            Console.WriteLine("Output description:");
        }

        /// <summary>
        /// This method displays a number.
        /// </summary>
        /// <param name="data">The data to display.</param>
        public void DisplayData(int[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} must not be null.");
            }

            if (data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} must contain at least one number.");
            }

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
        }
    }
}