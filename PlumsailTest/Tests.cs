using RomanEvaluationLibrary;

namespace RomanEvaluationTests;

public class Tests
{
    [TestCase("f", "'f'")]
    [TestCase("(I*(X - 1)) $", "'1', '$'")]
    public void IncorrectInput(string input, string expectedUnacceptables)
    {
        var exception = Assert.Throws<Exception>(() => RomanEvaluation.Evaluate(input));

        Assert.That(exception.Message, 
            Is.EqualTo($"Input contains unacceptable characters: [{expectedUnacceptables}]."));
    }

    [TestCase("V/III", "I")]
    [TestCase("-D / ( (CC - XC) / VI - XX)", "CCL")]
    public void NonIntegerDivisionIsAllowed(string input, string expectedEvaluation) => 
        Assert.That(RomanEvaluation.Evaluate(input, false), Is.EqualTo(expectedEvaluation));

    [Test]
    public void NonIntegerDivisionIsProhibited()
    {
        var exception = Assert.Throws<Exception>(() => RomanEvaluation.Evaluate("III/II", true));

        Assert.That(exception.Message, Is.EqualTo("Non-integer division is prohibited. " +
            "To allow it, pass throwOnNonIntegerDivision = false. [3] / [2]."));
    }

    [TestCase("I+I", "II")]
    [TestCase("II-I", "I")]
    [TestCase("I +I", "II")]
    [TestCase("(I)", "I")]
    [TestCase("((I))", "I")]
    [TestCase("I-II", "-I")]
    [TestCase("II*II", "IV")]
    [TestCase("I+II*II", "V")]
    [TestCase("VI/III", "II")]
    [TestCase("((I-V)*V-I)/(-VII)", "III")]
    [TestCase("0", "0")]
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
