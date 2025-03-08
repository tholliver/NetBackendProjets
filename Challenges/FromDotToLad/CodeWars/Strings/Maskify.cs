using System.Text;

namespace CodeWars.Strings;

public class Maskify
{
    public static string Mask(string cc)
    {
        int size = cc.Length;

        if (size <= 4)
            return cc;

        int hashTill = size - 4;
        var hashedString = new StringBuilder();

        for (int i = 0; i < cc.Length; i++)
        {
            if (hashTill <= i)
            {
                hashedString.Append(cc[i]);
            }
            else
                hashedString.Append('#');
        }
        return hashedString.ToString();
    }

    public static string MaskOp(string cc)
    {

        return "";
    }
}