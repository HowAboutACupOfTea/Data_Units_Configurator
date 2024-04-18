//-----------------------------------------------------------------------
// <copyright file="IDataProcessorUnit.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the IDataProcessorUnit interface.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace DataUnitsConfigurator
{
    /// <summary>
    /// This interface specifies the methods needed to be a data processor unit.
    /// </summary>
    /// <typeparam name="T">The type of the data that gets processed and the resulting data.</typeparam>
    public interface IDataProcessorUnit<T> : IUnitInterface
    {
        /// <summary>
        /// This method is needed to process data.
        /// </summary>
        /// <param name="data">The data to process.</param>
        /// <returns>The processed data.</returns>
        T ProcessData(T[] data);
    }
}
