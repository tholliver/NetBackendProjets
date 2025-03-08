namespace CodeWars.Sorts;

public class Quick
{
    public static List<int> SortQuick(List<int> arr)
    {
        if (arr.Count <= 1)
            return arr;

        int pivot = arr[arr.Count / 2];
        var left = new List<int>();
        var middle = new List<int>();
        var right = new List<int>();

        foreach (var i in arr)
        {
            if (i < pivot)
                left.Add(i);
            else if (i == pivot)
                middle.Add(i);
            else
                right.Add(i);
        }

        var sortedList = new List<int>();
        sortedList.AddRange(SortQuick(left));
        sortedList.AddRange(middle);
        sortedList.AddRange(SortQuick(right));

        return sortedList;
    }
}