//-----------------------------------------------------------------------
// <copyright file="NumberMultiplicator.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the NumberMultiplicator class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace MultiplyNumbers
{
    using System;

    /// <summary>
    /// This class implements functionality to process data.
    /// </summary>
    public class NumberMultiplicator : IDataProcessorUnit<int>
    {
        /// <summary>
        /// This method prints information about the class onto the console.
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine("Name: Number multiplier");
            Console.WriteLine("Description: Multiplies numbers.");
            Console.WriteLine("Input datatype: Int32[]");
            Console.WriteLine("Input description: The numbers to multiply.");
            Console.WriteLine("Output datatype: Int32");
            Console.WriteLine("Output description: The result of the multiplication.");
        }

        /// <summary>
        /// This method multiplies the given numbers.
        /// </summary>
        /// <param name="numbers">The numbers to multiply.</param>
        /// <returns>The multiplied number.</returns>
        public int ProcessData(int[] numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentNullException($"{nameof(numbers)} must not be null.");
            }

            if (numbers.Length == 0)
            {
                throw new ArgumentException($"{nameof(numbers)} must contain at least one number.");
            }

            int result = 1;

            for (int i = 0; i < numbers.Length; i++)
            {
                result *= numbers[i];
            }

            return result;
        }
    }
}
