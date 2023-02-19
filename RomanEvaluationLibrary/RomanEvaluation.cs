using Sprache;
using System.Data;

namespace RomanEvaluationLibrary;

public static class RomanEvaluation
{
    public static string Evaluate(string input, bool throwOnNonIntegerDivision = true)
    {
        var alphabet = RomanDigits.Select(x => x.roman).SelectMany(x => x).Distinct()
            .Concat(new[] { '+', '-', '*', '/', '(', ')', ' ', '0' }).ToArray();
        if (input.Distinct().Any(x => alphabet.Contains(x) == false))
        {
            var unacceptables = input.Distinct().Except(alphabet).ToArray();
            string unacceptablesString = string.Join(", ", unacceptables.Select(x => $"'{x}'"));
            throw new Exception($"Input contains unacceptable characters: [{unacceptablesString}].");
        }
        willThrowOnNonIntegerDivision = throwOnNonIntegerDivision;
        return IntToRoman(LinearOperationParser.Parse(input));
    }

    private static bool willThrowOnNonIntegerDivision = true;

    private static string IntToRoman(int number)
    {
        if (number == 0) return "0";
        var abs = Math.Abs(number);
        var result = "";
        var digitIndex = 0;
        while (abs > 0)
            if (abs >= RomanDigits[digitIndex].val)
            {
                abs -= RomanDigits[digitIndex].val;
                result += RomanDigits[digitIndex].roman;
            }
            else digitIndex++;
        if (number < 0) result = "-" + result;
        return result;
    }

    private static (string roman, int val)[] RomanDigits => new[]
    {
        ("M", 1000), ("CM", 900), ("D", 500), ("CD", 400), ("C", 100),
        ("XC", 90), ("L", 50), ("XL", 40), ("X", 10), ("IX", 9), ("V", 5), ("IV", 4), ("I", 1)
    };

    private static Parser<int> BaseDigitParser => Parse.String("0").Return(0);

    private static Parser<int> DigitParser
    {
        get
        {
            var digitsForParsing = RomanDigits.OrderByDescending(x => x.roman.Length).ToArray();
            var result = BaseDigitParser;
            foreach (var (roman, val) in digitsForParsing)
                result = result.Or(Parse.String(roman).Return(val));
            return result;
        }
    }

    private static Parser<int> NumberParser => DigitParser.Many().Token().Select(x => x.Sum());
    private static Parser<char> LinearOperationSignParser => Parse.Char('+').Or(Parse.Char('-'));
    private static Parser<char> NonLinearOperationSignParser => Parse.Char('*').Or(Parse.Char('/'));
    private static Parser<int> ExpressionParser =>
        SubexpressionParser.Or(NumberParser);
    private static Parser<int> NonLinearOperationParser =>
        Parse.ChainOperator(NonLinearOperationSignParser, ExpressionParser,
            (operatorSign, a, b) =>
            {
                if (operatorSign == '/' && a % b != 0 && willThrowOnNonIntegerDivision)
                    throw new Exception("Non-integer division is prohibited. " +
                        $"To allow it, pass throwOnNonIntegerDivision = false. [{a}] / [{b}].");
                return operatorSign switch
                {
                    '*' => a * b,
                    '/' => a / b,
                    _ => throw new Exception($"Unknown non-linear operator sign: [{operatorSign}].")
                };
            });
    private static Parser<int> LinearOperationParser =>
        Parse.ChainOperator(LinearOperationSignParser, NonLinearOperationParser,
            (operatorSign, a, b) => operatorSign switch
            {
                '+' => a + b,
                '-' => a - b,
                _ => throw new Exception($"Unknown linear operator sign: [{operatorSign}].")
            });
    private static Parser<int> SubexpressionParser =>
        from leftLimit in Parse.Char('(').Token()
        from expression in LinearOperationParser
        from rightLimit in Parse.Char(')').Token()
        select expression;
}