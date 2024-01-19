namespace Coding_Exercise_Andrea_Magnanini.Services
{
    using System.ComponentModel;
    using Enums;
    using Terms;

    public class CalculationService
    {
        public double GetResult(IList<ITerm> expression, ISignedNumber? previousResult)
        {
            if (expression.Count < 3 && previousResult == null)
            {
                throw new InvalidOperationException("Not enough terms were submitted - invalid expression.");
            }

            var operation = expression.Count == 3
                ? expression[1] as Operator
                : expression[0] as Operator;
            var firstOperand = expression.Count == 3
                ? expression[0] as SignedNumber
                : previousResult;
            var secondOperand = expression.Count == 3
                ? expression[2] as SignedNumber
                : expression[1] as SignedNumber;
            return operation!.Type switch
            {
                OperatorTypeEnum.Sum => firstOperand!.Value + secondOperand!.Value,
                OperatorTypeEnum.Sub => firstOperand!.Value - secondOperand!.Value,
                OperatorTypeEnum.Mul => firstOperand!.Value * secondOperand!.Value,
                OperatorTypeEnum.Div => firstOperand!.Value / secondOperand!.Value,
                _ => throw new InvalidEnumArgumentException("Operator recognition failed while performing operation."),
            };
        }
    }
}
