using Sprache;
using System.Data;

namespace PlumsailTest;

public class Tests
{
    [Test]
    public void SimplestAddition() =>
        Assert.That(Evaluate("I+I"), Is.EqualTo("II"));

    [TestCase("I")]
    [TestCase("II")]
    [TestCase("IV")]
    [TestCase("V")]
    [TestCase("IX")]
    [TestCase("X")]
    [TestCase("XL")]
    [TestCase("L")]
    [TestCase("XC")]
    [TestCase("C")]
    [TestCase("CD")]
    [TestCase("D")]
    [TestCase("CM")]
    [TestCase("M")]
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
        var digitIndex = 0;
        while (number > 0)
            if (number >= romanDigits[digitIndex].val)
            {
                number -= romanDigits[digitIndex].val;
                result += romanDigits[digitIndex].roman;
            }
            else digitIndex++;
        return result;
    }

    (string roman, int val)[] romanDigits = new[] 
    { 
        ("M", 1000), ("CM", 900), ("D", 500), ("CD", 400), ("C", 100), 
        ("XC", 90), ("L", 50), ("XL", 40), ("X", 10), ("IX", 9), ("V", 5), ("IV", 4), ("I", 1) 
    };
}