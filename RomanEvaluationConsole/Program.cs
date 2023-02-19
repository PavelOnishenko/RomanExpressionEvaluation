using RomanEvaluationLibrary;

string input;
do
{
    Console.WriteLine("Please enter roman expression to calculate. To exit, enter 'e'.");
    input = Console.ReadLine()!;
    if (input == "e") break;
    try
    {
        var result = RomanEvaluation.Evaluate(input, false);
        Console.WriteLine($"Evaluation result: [{result}].");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Exception occured: [{ex}].");
    }
} while (true);