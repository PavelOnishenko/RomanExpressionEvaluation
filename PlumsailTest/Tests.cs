using Sprache;
using System.Data;

namespace PlumsailTest;

public class Tests
{
    [TestCase("I+I", "II")]
    [TestCase("II-I", "I")]
    [TestCase("I +I", "II")]
    public void ExpressionParsing(string input, string expectedEvaluation) => 
        Assert.That(Evaluate(input), Is.EqualTo(expectedEvaluation));

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
        //todo add initial string and add iv to the digit info collection
        var digitParser = Parse.String("IV").Return(4);
        foreach (var tuple in digitsForParsing)
            digitParser = digitParser.Or(Parse.String(tuple.roman).Return(tuple.val));
        var numberParser = digitParser.Many().Token().Select(x => x.Sum());
        var plusParser = Parse.Char('+');
        var minusParser = Parse.Char('-');
        var operationSignParser = plusParser.Or(minusParser);
        var operationParser = Parse.ChainOperator(operationSignParser, numberParser,
            (op, a, b) => a + b * (op == '+' ? 1 : -1));
        var number = operationParser.Parse(input);
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