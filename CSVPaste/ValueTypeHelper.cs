using System;

namespace CSVPaste
{
    /// <summary>
    /// Helper class for working with the value types.
    /// </summary>
    internal class ValueTypeHelper
    {
        /// <summary>
        /// Determines the <see cref="ValueType"/> of the values and the existence of a header.
        /// </summary>
        public static ValueType DetermineValuesType(string[] values, out bool header)
        {
            header = false;

            // If the array contains only one element, return its type.
            if (values.Length == 1)
                return GetValueType(values[0]);

            ValueType? firstDeterminedType = null;
            for (var i = values.Length - 1; i >= 0; i--)
            {
                var type = GetValueType(values[i]);

                // Every item except the first.
                if (i != 0)
                {
                    // If any of the items is a string, then treat all of them as strings.
                    if (type == ValueType.String)
                    {
                        return ValueType.String;
                    }

                    // If the type of the item doesn’t match the first determined type, then there are different types of items in the selection.
                    // Treat all of them as strings.
                    if (firstDeterminedType.HasValue)
                    {
                        if (firstDeterminedType != type)
                        {
                            return ValueType.String;
                        }
                    }
                    else
                    {
                        firstDeterminedType = type;
                    }
                }
                else // The first item.
                {
                    // If the first item is a string and none of the previous items is one, then the selection contains a header.
                    // Return the type of the previous items (firstDeterminedType).
                    if (type == ValueType.String)
                    {
                        header = true;
                        return firstDeterminedType ?? ValueType.String;
                    }

                    // If the first item is not a string but it still doesn’t match the type of the previous items,
                    // then there are different types of items in the selection.
                    // Treat all of them as strings.
                    if (firstDeterminedType != type)
                    {
                        return ValueType.String;
                    }

                    // If the type of the first item matches the type of the previous items, return the type.
                    return firstDeterminedType.Value;
                }
            }

            return firstDeterminedType ?? ValueType.String;
        }

        /// <summary>
        /// Gets the <see cref="ValueType"/> of the value.
        /// </summary>
        private static ValueType GetValueType(string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
                return ValueType.Int;

            Guid guidValue;
            if (Guid.TryParse(value, out guidValue))
                return ValueType.Guid;

            return ValueType.String;
        }
    }
}