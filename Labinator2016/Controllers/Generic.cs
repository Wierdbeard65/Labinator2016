//-----------------------------------------------------------------------
// <copyright file="Generic.cs" company="Interactive Intelligence">
//     Copyright (c) Interactive Intelligence. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// Author: Paul.Simpson
/// Version: 1.0 - Initial build - 12/27/2015 6:31:34 PM
/// </summary>
namespace Labinator2016.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ViewModels.DatatablesViewModel;

    /// <summary>
    /// A bunch of classes that are used by the controllers to perform common, core tasks. A result of refactoring.
    /// </summary>
    public class Generic
    {
        /// <summary>
        /// Takes a List comprising all the possible (valid) records for a Data table and then sorts, filters and
        /// converts it to the object response to the original AJAX request.
        /// </summary>
        /// <typeparam name="T">The type of object in the DataTable</typeparam>
        /// <param name="allRecords">The Source record set.</param>
        /// <param name="param">The DTParameters object containing the formatting information.</param>
        /// <returns>A DTResult object containing the filtered and sorted data.</returns>
        public static DTResult<T> Ajax<T>(List<T> allRecords, DTParameters param) where T : class
        {
            string search = null;
            if (param.Search.Value != null)
            {
                search = param.Search.Value.ToLower();
            }

            List<T> displayedRecords = allRecords.Skip(param.Start).Take(param.Length).ToList();
            DTResult<T> result = new DTResult<T>
            {
                draw = param.Draw,
                data = displayedRecords,
                recordsFiltered = allRecords.Count(),
                recordsTotal = allRecords.Count()
            };
            return result;
        }
    }
}