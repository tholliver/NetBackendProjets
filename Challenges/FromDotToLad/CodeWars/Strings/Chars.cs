namespace CodeWars.Strings;

public class Chars
{
    public static int CountRepeats(string cad)
    {
        Dictionary<char, int> chars = [];

        foreach (var i in cad.ToLower())
        {
            if (chars.ContainsKey(i))
                chars[i]++;
            else
                chars.Add(i, 1);
        }

        return CounterCharApp(chars);
    }

    public static int CounterCharApp(Dictionary<char, int> chars)
    {
        var total = 0;
        foreach (var item in chars)
        {
            if (item.Value > 1)
                total++;
        }

        return total;
    }

    /// Improved 
    public static int DuplicatedCharCount(string str)
    {
        return str.ToLower()
                  .GroupBy(c => c)
                  .Where(g => g.Count() > 1)
                  .Count();
    }
}