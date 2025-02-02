using StringsDir;
public class Program
{
    public static void Main()
    {
        string res = SnakeToCamel.ToCamelCase("hello_world");
        string res1 = SnakeToCamel.ToCamelCaseLinQ("hello_world");

        Console.WriteLine($"{res} === {res1}");
        Console.WriteLine($"{SnakeToCamel.TestLinQ("hello_world")}");
    }
}

