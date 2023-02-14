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
    public void OneDigit(string romanNumber) => 
        Assert.That(Evaluate(romanNumber), Is.EqualTo(romanNumber));

    private string Evaluate(string input)
    {
        var digitInfo = new (string roman, int val)[]
        {("IX", 9), ("I", 1), ("V", 5), ("X", 10)};
        var parser = Parse.String("IV").Return(4);
        foreach(var tuple in digitInfo)
            parser = parser.Or(Parse.String(tuple.roman).Return(tuple.val));
        var number = parser.Many().Select(x => x.Sum()).Parse(input);
        return number switch 
        { 
            1 => "I", 2 => "II", 4 => "IV", 5 => "V", 9 => "IX", 10 => "X",
            _ => throw new Exception($"Can't convert {number} to roman number.") 
        };
    }
}