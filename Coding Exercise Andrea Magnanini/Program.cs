using System.Globalization;
using Coding_Exercise_Andrea_Magnanini.Factories;
using Coding_Exercise_Andrea_Magnanini.Services;
using Coding_Exercise_Andrea_Magnanini.Terms;

const string usageMessage = "Welcome to this Coding Example by Andrea Magnanini.\n\n" +
                            "************************ USAGE *************************\n\n" +
                            "> Input any two-operands expression and get its result.\n" +
                            "> Concatenate a second operand to a previous result and\n" +
                            "  calculate the new result.\n" +
                            "> Enter 'ac' to clear data and 'exit' to quit.\n\n" +
                            "********************************************************\n";

Console.WriteLine(usageMessage);
var input = string.Empty;
SignedNumber? previousResult = null;
var validationService = new ValidationService(new TermFactory());
var calculationService = new CalculationService();

while (!input.Trim().Equals("exit", StringComparison.InvariantCultureIgnoreCase))
{
    Console.Write("> ");
    input = Console.ReadLine();
    if (input!.Trim().Equals("AC", StringComparison.InvariantCultureIgnoreCase))
    {
        Console.Clear();
        Console.WriteLine(usageMessage);
        previousResult = null;
    }
    else
    {
        if (!input.Trim().Equals("exit", StringComparison.InvariantCultureIgnoreCase))
        {
            try
            {
                var expression = validationService.BuildExpression(input);
                var result = calculationService.GetResult(expression, previousResult);
                Console.WriteLine(double.IsInfinity(result)
                    ? result > 0 ? "Infinity" : "-Infinity"
                    : result.ToString(CultureInfo.GetCultureInfo("en-US")));
                previousResult = new SignedNumber(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                previousResult = null;
            }
        }
    }
}