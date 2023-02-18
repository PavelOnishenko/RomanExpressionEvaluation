using Sprache;
using System.Data;

namespace PlumsailTest;

public class Tests
{
    [TestCase("I+I", "II")]
    [TestCase("II-I", "I")]
    [TestCase("I +I", "II")]
    [TestCase("(I)", "I")]
    [TestCase("((I))", "I")]
    public void ExpressionParsing(string input, string expectedEvaluation) => 
        Assert.That(RomanEvaluation.Evaluate(input), Is.EqualTo(expectedEvaluation));

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
        Assert.That(RomanEvaluation.Evaluate(romanNumber), Is.EqualTo(romanNumber));
}

class RomanEvaluation
{
    public static string Evaluate(string input) => IntToRoman(OperationParser.Parse(input));

    private static string IntToRoman(int number)
    {
        var result = "";
        var digitIndex = 0;
        while (number > 0)
            if (number >= RomanDigits[digitIndex].val)
            {
                number -= RomanDigits[digitIndex].val;
                result += RomanDigits[digitIndex].roman;
            }
            else digitIndex++;
        return result;
    }

    private static (string roman, int val)[] RomanDigits => new[]
    {
        ("M", 1000), ("CM", 900), ("D", 500), ("CD", 400), ("C", 100),
        ("XC", 90), ("L", 50), ("XL", 40), ("X", 10), ("IX", 9), ("V", 5), ("IV", 4), ("I", 1)
    };

    //todo add initial string and add iv to the digit info collection
    private static Parser<int> BaseDigitParser => Parse.String("IV").Return(4);

    private static Parser<int> DigitParser
    {
        get
        {
            var digitsForParsing = RomanDigits.OrderByDescending(x => x.roman.Length).ToArray();
            var result = BaseDigitParser;
            foreach (var tuple in digitsForParsing)
                result = result.Or(Parse.String(tuple.roman).Return(tuple.val));
            return result;
        }
    }
    private static Parser<int> NumberParser => DigitParser.Many().Token().Select(x => x.Sum());
    private static Parser<char> OperationSignParser => Parse.Char('+').Or(Parse.Char('-'));
    private static Parser<int> ExpressionParser =>
        SubexpressionParser.Or(NumberParser);
    private static Parser<int> OperationParser => 
        Parse.ChainOperator(OperationSignParser, ExpressionParser,
            (op, a, b) => a + b * (op == '+' ? 1 : -1));
    private static Parser<int> SubexpressionParser =>
        from leftLimit in Parse.Char('(').Token()
        from expression in OperationParser
        from rightLimit in Parse.Char(')').Token()
        select expression;
}