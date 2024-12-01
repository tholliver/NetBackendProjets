using System.Reflection;

namespace TCPExtensions;
public static class Extension
{
    public static List<T> Filter<T>(this List<T> records,
                                     Func<T, bool> function)
    {
        List<T> filteredList = [];
        foreach (T record in records)
        {
            if (function(record))
            {
                filteredList.Add(record);
            }
        }
        return filteredList;
    }

    public static void Print<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine("-------------");
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                var value = prop.GetValue(item, null) ?? "null";
                Console.WriteLine($"{prop.Name}: {value}");
            }
            Console.WriteLine();
        }
    }
}






