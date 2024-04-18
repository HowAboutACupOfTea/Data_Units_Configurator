//-----------------------------------------------------------------------
// <copyright file="NumberGenerator.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the NumberGenerator class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace RandomValueGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// This class implements functionality to generate data.
    /// </summary>
    public class NumberGenerator : IDataSourceUnit<IEnumerable<int>>
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
            Console.WriteLine("Name: Random number generator ");
            Console.WriteLine("Description: Generates one random value between 1 and 10 every second.");
            Console.WriteLine("Input datatype: None");
            Console.WriteLine("Input description:");
            Console.WriteLine("Output datatype: IEnumerable");
            Console.WriteLine("Output description: A random value between 1 and 10.");
        }

        /// <summary>
        /// This method continually generates a random number between 1 and 10.
        /// </summary>
        /// <returns>The generated number.</returns>
        public IEnumerable<int> GenerateOutput()
        {           
            while (true)
            {
                yield return this.Random.Next(1, 11);
                Thread.Sleep(1000);
            }
        }
    }
}
