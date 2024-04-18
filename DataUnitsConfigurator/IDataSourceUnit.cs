//-----------------------------------------------------------------------
// <copyright file="IDataSourceUnit.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the IDataSourceUnit interface.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace DataUnitsConfigurator
{
    /// <summary>
    /// This interface specifies the methods needed to be a data source unit.
    /// </summary>
    /// <typeparam name="T">The type of the data that gets generated.</typeparam>
    public interface IDataSourceUnit<T> : IUnitInterface
    {
        /// <summary>
        /// This method is needed to generate data.
        /// </summary>
        /// <returns>The generated data.</returns>
        T GenerateOutput();
    }
}
