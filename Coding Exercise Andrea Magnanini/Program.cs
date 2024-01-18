using Coding_Exercise_Andrea_Magnanini;
using Coding_Exercise_Andrea_Magnanini.Terms;
using System.Globalization;

const string usageMessage = "Welcome to this Coding Example by Andrea Magnanini\n\n" +
                   "************************ USAGE *************************\n\n" +
                   "Input any two-operands expression and get the result or\n" +
                   "concatenate a second operand to the result of a\n" +
                   "previous input.\n" +
                   "Enter AC to clear data and exit to quit.\n\n" +
                   "********************************************************";
Console.WriteLine(usageMessage);
var input = string.Empty;
var termFactory = new TermFactory();
var validationService = new ValidationService(termFactory);
var calculationService = new CalculationService();
SignedNumber? previousResult = null;
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