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
    public void OneDigit(string romanNumber) =>
        Assert.That(Evaluate(romanNumber), Is.EqualTo(romanNumber));

    private string Evaluate(string input)
    {
        var digitInfo = new (string roman, int val)[]
        {("IX", 9), ("XL", 40), ("I", 1), ("V", 5), ("X", 10)};
        var parser = Parse.String("IV").Return(4);
        foreach (var tuple in digitInfo)
            parser = parser.Or(Parse.String(tuple.roman).Return(tuple.val));
        var number = parser.Many().Select(x => x.Sum()).Parse(input);
        return IntToRoman(number);
    }

    private string IntToRoman(int number)
    {
        var result = "";
        var digitInfo = new (string roman, int val)[]
        { ("XL", 40), ("X", 10), ("IX", 9), ("V", 5), ("IV", 4), ("I", 1) };
        var i = 0;
        while (number > 0)
        {
            if (number >= digitInfo[i].val)
            {
                number -= digitInfo[i].val;
                result += digitInfo[i].roman;
            }
            else i++;
        }
        return result;
    }
}