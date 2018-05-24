using System;
using System.Collections.Generic;

namespace CSVPaste
{
    /// <summary>
    /// Helper class for formatting the raw clipboard text.
    /// </summary>
    internal class TextHelper
    {
        /// <summary>
        /// Gets the values as a formatted comma separated list.
        /// </summary>
        /// <param name="text">The raw clipboard text.</param>
        public static string GetFormattedText(string text)
        {
            var allValues = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var values = RemoveNullValues(allValues);

            bool header;
            var type = ValueTypeHelper.DetermineValuesType(values, out header);

            if (!header)
            {
                return JoinHelper.JoinValues(values, type);
            }

            var valuesWithoutHeader = new string[values.Length - 1];
            Array.Copy(values, 1, valuesWithoutHeader, 0, values.Length - 1);

            return JoinHelper.JoinValues(valuesWithoutHeader, type);
        }

        /// <summary>
        /// Returns a list without the NULL values.
        /// </summary>
        /// <param name="values">The raw values.</param>
        private static string[] RemoveNullValues(string[] values)
        {
            var filteredValues = new List<string>();

            foreach (var value in values)
            {
                if (!value.Equals("NULL"))
                {
                    filteredValues.Add(value);
                }
            }

            return filteredValues.ToArray();
        }
    }
}