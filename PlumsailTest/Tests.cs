using Sprache;

namespace PlumsailTest;

public class Tests
{
    [TestCase("I")]
    [TestCase("II")]
    [TestCase("IV")]
    public void LiteralParsing(string romanNumber)
    {
        Assert.That(Evaluate(romanNumber), Is.EqualTo(romanNumber));
    }

    private string Evaluate(string input)
    {
        var number = Parse.String("IV").Return(4).Or(Parse.String("I").Return(1))
            .Many().Select(x => x.Sum()).Parse(input);
        return number switch 
        { 
            1 => "I", 2 => "II", 4 => "IV", 
            _ => throw new Exception($"Can't convert {number} to roman number.") 
        };
    }
}