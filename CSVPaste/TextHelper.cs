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
        public static string GetFormattedText(string text)
        {
            var values = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            bool header;
            var type = ValueTypeHelper.DetermineValuesType(values, out header);

            return type.ToString();
        }
    }
}