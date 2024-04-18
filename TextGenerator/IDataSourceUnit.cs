//-----------------------------------------------------------------------
// <copyright file="IDataSourceUnit.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the IDataSourceUnit interface.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace TextGenerator
{
    /// <summary>
    /// This interface specifies the methods needed to be a data source unit.
    /// </summary>
    /// <typeparam name="T">The type of the data that gets generated.</typeparam>
    public interface IDataSourceUnit<T>
    {
        /// <summary>
        /// This method is needed to print information about itself onto the console.
        /// </summary>
        void ShowInformation();

        /// <summary>
        /// This method is needed to generate data.
        /// </summary>
        /// <returns>The generated data.</returns>
        T GenerateOutput();
    }
}