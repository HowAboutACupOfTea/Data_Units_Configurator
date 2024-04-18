//-----------------------------------------------------------------------
// <copyright file="DataDisplayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the DataDisplayer class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace DataVisualizer
{
    using System;

    /// <summary>
    /// This class implements functionality to display strings via the console.
    /// </summary>
    public class DataDisplayer : IDataVisualizerUnit<object>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: Data displayer");
            Console.WriteLine("Description: Displays data in text form.");
            Console.WriteLine("Input datatype: object[]");
            Console.WriteLine("Input description: The data to display.");
            Console.WriteLine("Output datatype: none");
            Console.WriteLine("Output description:");
        }

        /// <summary>
        /// This method displays objects as string onto the console.
        /// </summary>
        /// <param name="data">The data to display.</param>
        public void DisplayData(object[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} must not be null.");
            }

            if (data.Length == 0)
            {
                throw new ArgumentException($"{nameof(data)} must contain at least one object.");
            }

            for (int i = 0; i < data.Length; i++)
            {
                try
                {
                    Console.WriteLine(data[i].ToString());
                }
                catch (Exception)
                {
                    throw new Exception($"A problem arose while trying to use the ToString method of {nameof(data)}.");
                }
            }
        }
    }
}
