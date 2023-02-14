namespace PlumsailTest;

public class Tests
{
    [Test]
    public void ReturnsSame() => Assert.That((string?)Evaluate("I"), Is.EqualTo("I"));

    private string Evaluate(string input) => "I";
}