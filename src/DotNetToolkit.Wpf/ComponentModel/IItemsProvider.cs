﻿namespace DotNetToolkit.Wpf.ComponentModel.DataVirtualization
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a provider of collection details.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public interface IItemsProvider<T>
    {
        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns>The count.</returns>
        int FetchCount();

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageCount">The number of items to fetch.</param>
        /// <param name="overallCount">The total number of items.</param>
        /// <returns>The fetched items.</returns>
        IList<T> FetchRange(int startIndex, int pageCount, out int overallCount);
    }
}
