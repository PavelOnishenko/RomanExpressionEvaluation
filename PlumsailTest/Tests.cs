using Sprache;

namespace PlumsailTest;

public class Tests
{
    [Test]
    public void ParseIi() => Assert.That(Evaluate("II"), Is.EqualTo("II"));

    [Test]
    public void ParseI() => Assert.That(Evaluate("I"), Is.EqualTo("I"));

    private string Evaluate(string input)
    {
        var number = Parse.String("I").Return(1).Many().Select(x => x.Sum()).Parse(input);
        return number switch 
        { 1 => "I", 2 => "II", _ => throw new Exception($"Can't convert {number} to roman number.") };
    }
}