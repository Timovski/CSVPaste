using System.Text;

namespace CSVPaste
{
    /// <summary>
    /// Helper class for joining the raw values.
    /// </summary>
    internal class JoinHelper
    {
        /// <summary>
        /// Formats and joins the raw values into a formatted string.
        /// </summary>
        /// <param name="values">The raw values.</param>
        /// <param name="type">The values type.</param>
        public static string JoinValues(string[] values, ValueType type)
        {
            var builder = new StringBuilder();

            if (type == ValueType.Guid)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    if (i != values.Length - 1)
                    {
                        if (i % 2 == 0)
                        {
                            builder.Append($"'{values[i]}', ");
                        }
                        else
                        {
                            builder.AppendLine($"'{values[i]}',");
                        }
                    }
                    else
                    {
                        builder.Append($"'{values[i]}'");
                    }
                }
            }
            else if (type == ValueType.Numeric)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    if (i != values.Length - 1)
                    {
                        if ((i + 1) % 5 != 0)
                        {
                            builder.Append($"{values[i]}, ");
                        }
                        else
                        {
                            builder.AppendLine($"{values[i]},");
                        }
                    }
                    else
                    {
                        builder.Append($"{values[i]}");
                    }
                }
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                {
                    if (i != values.Length - 1)
                    {
                        builder.AppendLine($"N'{values[i]}',");
                    }
                    else
                    {
                        builder.Append($"N'{values[i]}'");
                    }
                }
            }

            return builder.ToString();
        }
    }
}