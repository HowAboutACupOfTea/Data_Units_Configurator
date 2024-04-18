﻿//-----------------------------------------------------------------------
// <copyright file="IDataVisualizerUnit.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the IDataVisualizerUnit interface.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace DataUnitsConfigurator
{
    /// <summary>
    /// This interface specifies the methods needed to be a data visualizer unit.
    /// </summary>
    /// <typeparam name="T">The type of the data that gets visualized.</typeparam>
    public interface IDataVisualizerUnit<T> : IUnitInterface
    {
        /// <summary>
        /// This method is needed to display data.
        /// </summary>
        /// <param name="data">The data to display.</param>
        void DisplayData(T[] data);
    }
}
