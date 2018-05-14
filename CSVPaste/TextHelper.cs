using System;

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
            var values = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

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
    }
}