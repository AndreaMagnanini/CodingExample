using Coding_Exercise_Andrea_Magnanini;
using Coding_Exercise_Andrea_Magnanini.Terms;
using System.Globalization;

const string usageMessage = "Welcome to this Coding Example by Andrea Magnanini.\n\n" +
                   "************************ USAGE *************************\n\n" +
                   "> Input any two-operands expression and get its result.\n" +
                   "> Concatenate a second operand to a previous result and\n" +
                   "  get its new value.\n" +
                   "> Enter 'ac' to clear data and 'exit' to quit.\n\n" +
                   "********************************************************\n";

Console.WriteLine(usageMessage);
var input = string.Empty;
SignedNumber? previousResult = null;
var validationService = new ValidationService(new TermFactory());
var calculationService = new CalculationService();

while (!input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
{
    Console.Write("> ");
    input = Console.ReadLine();
    if (input!.Equals("AC", StringComparison.InvariantCultureIgnoreCase))
    {
        Console.Clear();
        Console.WriteLine(usageMessage);
        previousResult = null;
    }
    else
    {
        if (!input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
        {
            try
            {
                var expression = validationService.BuildExpression(input);
                var result = calculationService.GetResult(expression, previousResult);
                if (double.IsInfinity(result))
                {
                    previousResult = null;
                    Console.WriteLine("Infinity");
                }
                else
                {
                    previousResult = new SignedNumber(result);
                    Console.WriteLine(result.ToString(CultureInfo.GetCultureInfo("en-US")));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}