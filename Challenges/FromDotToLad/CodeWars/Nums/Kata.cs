namespace CodeWars.Nums;

public static class Kata
{
    public static string OrderStringDescending(this string str)
    {
        return string.Concat(str.OrderByDescending(c => c));
    }

    public static int DescendingOrder(int num)
    {
        return int.Parse(num.ToString().OrderStringDescending());
    }
}