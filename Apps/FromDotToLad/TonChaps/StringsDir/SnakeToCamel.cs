using System.Text;

namespace StringsDir;
public class SnakeToCamel
{
    public static string ToCamelCase(string snakeString)
    {
        var wordList = new StringBuilder();
        for (int i = 0; i < snakeString.Length; i++)
        {
            if (snakeString[i].Equals('_'))
            {
                wordList.Append(snakeString[i + 1].ToString().ToUpper());
                i += 2;
            }

            wordList.Append(snakeString[i]);
        }
        return wordList.ToString();
    }

    public static string ToCamelCaseInproved(string snakeString)
    {
        if (string.IsNullOrEmpty(snakeString)) return snakeString;

        var wordList = new StringBuilder();
        bool capitalizeNext = false;

        foreach (char c in snakeString)
        {
            if (c == '_')
            {
                capitalizeNext = true; // Next character should be uppercase
            }
            else
            {
                wordList.Append(capitalizeNext ? char.ToUpper(c) : c);
                capitalizeNext = false; // Reset flag
            }
        }

        return wordList.ToString();
    }

    public static string ToCamelCaseLinQ(string snakeString)
    {
        return string.Join(
            string.Empty, snakeString.Split('_', StringSplitOptions.RemoveEmptyEntries)
            .Select((word, i) => i == 0
                    ? word.ToLowerInvariant()
                    : char.ToUpperInvariant(word[0]) + word.Substring(1).ToLowerInvariant()
             ));
    }

    public static string TestLinQ(string snakeString)
    {
        return string.Join(string.Empty,
                           snakeString.Split('_', StringSplitOptions.RemoveEmptyEntries));
    }
}