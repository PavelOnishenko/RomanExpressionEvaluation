using NUnit.Framework.Constraints;
using Sprache;

namespace PlumsailTest;

public class Tests
{
    [Test]
    public void ParseI()
    {
        Assert.That(Evaluate("I"), Is.EqualTo("I"));
    }

    private string Evaluate(string input)
    {
        var parser = Parse.String("I").Return(1);
        var digit = parser.Parse(input);
        var result = digit == 1 ? "I" : throw new NotImplementedException();
        return result;
    }
}

//private static readonly Parser<Digit> digitParserIv =
//      Parse.String("IV").Return(new Digit(4));

//private static readonly Parser<Digit> digitParserV =
//    Parse.String("V").Return(new Digit(5));

//private static readonly Parser<Digit> digitParserIx =
//    Parse.String("IX").Return(new Digit(9));

//private static readonly Parser<Digit> digitParserX =
//    Parse.String("X").Return(new Digit(10));

//private static readonly Parser<Digit> digitParserXl =
//    Parse.String("XL").Return(new Digit(40));

//private static readonly Parser<Digit> digitParserL =
//    Parse.String("L").Return(new Digit(50));

//private static readonly Parser<Digit> digitParserXc =
//    Parse.String("XC").Return(new Digit(90));

//private static readonly Parser<Digit> digitParserC =
//    Parse.String("C").Return(new Digit(100));

//private static readonly Parser<Digit> digitParserCd =
//    Parse.String("CD").Return(new Digit(400));

//private static readonly Parser<Digit> digitParserD =
//    Parse.String("D").Return(new Digit(500));

//private static readonly Parser<Digit> digitParserCm =
//    Parse.String("CM").Return(new Digit(900));

//private static readonly Parser<Digit> digitParserM =
//    Parse.String("M").Return(new Digit(1000));

//private static readonly Parser<Digit> digitParser =
//    digitParserI
//    .Or(digitParserIv)
//    .Or(digitParserV)
//    .Or(digitParserIx)
//    .Or(digitParserX)
//    .Or(digitParserXl)
//    .Or(digitParserL)
//    .Or(digitParserXc)
//    .Or(digitParserC)
//    .Or(digitParserCd)
//    .Or(digitParserD)
//    .Or(digitParserCm)
//    .Or(digitParserM);