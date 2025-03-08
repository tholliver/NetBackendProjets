// See https://aka.ms/new-console-template for more information
using CodeWars.Nums;
using CodeWars.Sorts;
using CodeWars.Strings;

var kata = Kata.DescendingOrder(1230);
// Console.WriteLine(kata);

var sortedList = Quick.SortQuick([2, 5, 5, 8, 22, 1]);
// Console.Write(string.Join(", ", sortedList));

// var repeatedChars = Chars.CountRepeats("aabBcde");
// Console.WriteLine(repeatedChars);

string maskedString = Maskify.Mask("4");
Console.WriteLine(maskedString);
