namespace PlumsailTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReturnsSomething()
        {
            Assert.DoesNotThrow(() => Evaluate("I"));
        }

        private string Evaluate(string input) 
        {
            return null;
        }
    }
}