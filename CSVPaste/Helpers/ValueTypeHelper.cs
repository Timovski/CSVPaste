using System;

namespace CSVPaste.Helpers
{
    /// <summary>
    /// Helper class for working with the value types.
    /// </summary>
    internal class ValueTypeHelper
    {
        /// <summary>
        /// Determines the <see cref="ValueType"/> of the values and the existence of a header.
        /// </summary>
        /// <param name="values">The raw values.</param>
        /// <param name="header">Flag indicating whether the column header was present in the values or not.</param>
        /// <returns>The determined <see cref="ValueType"/> for all the values.</returns>
        public static ValueType DetermineValuesType(string[] values, out bool header)
        {
            header = false;

            // Determine the type of the last item (the first item from the back).
            // If the type is text, treat all the values as text.
            var initialType = GetValueType(values[values.Length - 1]);
            if (initialType == ValueType.Text)
                return ValueType.Text;

            // If the array contains only one element, return its type.
            if (values.Length == 1)
                return initialType;

            // For every item except the first and the last, check if their type matches the initially determined one.
            // If is doesn’t, treat all the values as text.
            for (var i = values.Length - 2; i >= 1; i--)
            {
                if (!EqualsValueType(values[i], initialType))
                {
                    return ValueType.Text;
                }
            }

            // Determine the type of the first item.
            // If the first item is text but none of the previous items is, then the selection contains a header.
            var firstItemType = GetValueType(values[0]);
            if (firstItemType == ValueType.Text)
            {
                header = true;
                return initialType;
            }

            // If the first item is not text but it still doesn’t match the type of the previous items,
            // then there are different types of items in the selection.
            // Treat all of them as text.
            if (!EqualsValueType(values[0], initialType))
            {
                return ValueType.Text;
            }

            return initialType;
        }

        /// <summary>
        /// Gets the <see cref="ValueType"/> of the value.
        /// </summary>
        /// <param name="value">The raw value.</param>
        /// <returns>The determined <see cref="ValueType"/> for the value.</returns>
        private static ValueType GetValueType(string value)
        {
            decimal decimalValue;
            if (decimal.TryParse(value, out decimalValue))
                return ValueType.Numeric;

            double doubleValue;
            if (double.TryParse(value, out doubleValue))
                return ValueType.Numeric;

            Guid guidValue;
            if (Guid.TryParse(value, out guidValue))
                return ValueType.Uniqueidentifier;

            DateTime dateTimeValue;
            if (DateTime.TryParse(value, out dateTimeValue))
                return ValueType.DateTime;

            return ValueType.Text;
        }

        /// <summary>
        /// Determines whether the value is of particular <see cref="ValueType"/>.
        /// </summary>
        /// <param name="value">The raw value.</param>
        /// <param name="valueType">The <see cref="ValueType"/>.</param>
        /// <returns>true if the <param name="value" /> parameter is of the provided <see cref="ValueType"/>.</returns>
        private static bool EqualsValueType(string value, ValueType valueType)
        {
            switch (valueType)
            {
                case ValueType.Numeric:
                    decimal decimalValue;
                    var isDecimal = decimal.TryParse(value, out decimalValue);
                    if (isDecimal)
                        return true;

                    double doubleValue;
                    return double.TryParse(value, out doubleValue);
                case ValueType.Uniqueidentifier:
                    Guid guidValue;
                    return Guid.TryParse(value, out guidValue);

                case ValueType.DateTime:
                    DateTime dateTimeValue;
                    return DateTime.TryParse(value, out dateTimeValue);
            }

            return true;
        }
    }
}