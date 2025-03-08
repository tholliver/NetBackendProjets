using System.ComponentModel.DataAnnotations;

namespace LinQMax.Utils
{
    public class PrintAny
    {
        public static void Print<T>(T item)
        {
            if (item == null)
            {
                Console.WriteLine("No data to print.");
                return;
            }

            if (item is IEnumerable<object> enumerable)  // Check if it's a list or array
            {
                PrintEnumerable(enumerable);
            }
            else
            {
                PrintSingle(item);
            }
        }

        // For lists/arrays
        public static void PrintEnumerable(IEnumerable<object> items)
        {
            if (items == null || !items.Any())
            {
                Console.WriteLine("No data to print.");
                return;
            }

            // If items are simple types, join and print them directly
            if (IsSimpleType(items.First().GetType()))
            {
                Console.WriteLine(string.Join(", ", items));
            }
            else
            {
                PrintObjects(items);
            }
        }

        // For a single value (basic type or object)
        public static void PrintSingle<T>(T item)
        {
            if (IsSimpleType(typeof(T)))
            {
                Console.WriteLine(item);
            }
            else
            {
                // If it's an array, process it as an array of values
                if (item is Array arrayItem)
                {
                    // Join the elements in the array
                    Console.WriteLine(string.Join(", ", arrayItem.Cast<object>()));
                }
                else
                {
                    PrintObjects(new object[] { item });
                }
            }
        }

        // To print objects (anonymous types or regular objects)
        public static void PrintObjects(IEnumerable<object> items)
        {
            if (!items.Any()) return;

            var properties = items.First().GetType().GetProperties();

            // Header
            Console.WriteLine(string.Join(", ", properties.Select(p => p.Name)));

            // Rows
            foreach (var item in items)
            {
                var values = properties.Select(p =>
                {
                    try
                    {
                        // Safely get value
                        var value = p.GetValue(item);
                        return value?.ToString() ?? "";
                    }
                    catch (Exception ex)
                    {
                        // In case of an error, log it
                        Console.WriteLine($"Error accessing property {p.Name}: {ex.Message}");
                        return "";
                    }
                });
                Console.WriteLine(string.Join(", ", values));
            }
        }

        // Helper to detect simple types
        public static bool IsSimpleType(Type type) =>
            type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);
    }

    public static class TablePrinter
    {
        public static readonly Dictionary<string, dynamic> tableStyles = new Dictionary<string, dynamic>
{
    {
        "rounded", new
        {
            rightTop = '╮',
            leftTop = '╭',
            bottomLeft = '╰',
            bottomRight = '╯'
        }
    },
    {
        "squared", new
        {
            rightTop = '┐',
            leftTop = '┌',
            bottomLeft = '└',
            bottomRight = '┘'
        }
    },
    {
        "custom", new
        {
            rightTop = '+',
            leftTop = '+',
            bottomLeft = '+',
            bottomRight = '+'
        }
    }
};

        /// <summary> 
        /// Prints the body of a table in a formatted style based on the selected border style.
        /// </summary>
        /// <param name="items">The collection of objects to be printed in the table body.</param>
        /// <param name="style">The style of the table border. Defaults to "rounded".</param>
        public static void Display<TSource>(this IEnumerable<TSource> source,
                                            string style = "rounded")
        {
            if (source == null || !source.Any())
            {
                Console.WriteLine("No items to print.");
                return;
            }

            var selectedStyle = tableStyles[style];

            var itemProperties = source.First().GetType().GetProperties();

            var columnWidths = itemProperties.Select(p => p.Name.Length).ToArray();

            foreach (var item in source)
            {
                for (int i = 0; i < itemProperties.Length; i++)
                {
                    // Get the value of the property and update the column width to the maximum
                    var value = itemProperties[i].GetValue(item)?.ToString() ?? string.Empty;
                    columnWidths[i] = Math.Max(columnWidths[i], value.Length);
                }
            }

            Console.WriteLine(selectedStyle.leftTop + string.Join("┬", columnWidths.Select(w => new string('─', w + 2))) + selectedStyle.rightTop);
            Console.WriteLine("│ " + string.Join(" │ ", itemProperties.Select((p, i) => p.Name.PadRight(columnWidths[i]))) + " │");
            Console.WriteLine("├" + string.Join("┼", columnWidths.Select(w => new string('─', w + 2))) + "┤");

            foreach (var item in source)
            {
                Console.WriteLine("│ " + string.Join(" │ ", itemProperties.Select((p, i) =>
                {
                    var value = p.GetValue(item)?.ToString() ?? string.Empty;
                    return value.PadRight(columnWidths[i]);
                })) + " │");
            }

            Console.WriteLine(selectedStyle.bottomLeft + string.Join("┴", columnWidths.Select(w => new string('─', w + 2))) + selectedStyle.bottomRight);
        }
    }
}
