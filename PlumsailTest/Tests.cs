using Sprache;
using System.Data;

namespace PlumsailTest;

public class Tests
{
    [TestCase("I")]
    [TestCase("II")]
    [TestCase("IV")]
    [TestCase("V")]
    [TestCase("IX")]
    [TestCase("X")]
    [TestCase("XL")]
    [TestCase("L")]
    public void OneDigit(string romanNumber) =>
        Assert.That(Evaluate(romanNumber), Is.EqualTo(romanNumber));

    private string Evaluate(string input)
    {
        var digitsForParsing = romanDigits.OrderByDescending(x => x.roman.Length).ToArray();
        var parser = Parse.String("IV").Return(4);
        foreach (var tuple in digitsForParsing)
            parser = parser.Or(Parse.String(tuple.roman).Return(tuple.val));
        var number = parser.Many().Select(x => x.Sum()).Parse(input);
        return IntToRoman(number);
    }

    private string IntToRoman(int number)
    {
        var result = "";
        var i = 0;
        while (number > 0)
            if (number >= romanDigits[i].val)
            {
                number -= romanDigits[i].val;
                result += romanDigits[i].roman;
            }
            else i++;
        return result;
    }

    (string roman, int val)[] romanDigits = 
        new[] { ("L", 50), ("XL", 40), ("X", 10), ("IX", 9), ("V", 5), ("IV", 4), ("I", 1) };
}