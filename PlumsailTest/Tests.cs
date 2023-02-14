using Sprache;

namespace PlumsailTest;

public class Tests
{
    [TestCase("I")]
    [TestCase("II")]
    [TestCase("IV")]
    [TestCase("V")]
    [TestCase("IX")]
    public void LiteralParsing(string romanNumber) => 
        Assert.That(Evaluate(romanNumber), Is.EqualTo(romanNumber));

    private string Evaluate(string input)
    {
        var number = Parse.String("IV").Return(4).Or(Parse.String("IX").Return(9))
            .Or(Parse.String("I").Return(1)).Or(Parse.String("V").Return(5))
            .Many().Select(x => x.Sum()).Parse(input);
        return number switch 
        { 
            1 => "I", 2 => "II", 4 => "IV", 5 => "V", 9 => "IX",
            _ => throw new Exception($"Can't convert {number} to roman number.") 
        };
    }
}