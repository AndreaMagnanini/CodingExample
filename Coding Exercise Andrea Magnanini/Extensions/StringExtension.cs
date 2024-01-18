namespace Coding_Exercise_Andrea_Magnanini.Extensions
{
    using Enums;
    using System.ComponentModel;

    public static class StringExtension
    {
        public static OperatorTypeEnum ToOperatorType(this string character) => character switch
        {
            "+" => OperatorTypeEnum.Sum,
            "-" => OperatorTypeEnum.Sub,
            "*" or "x" or "X" => OperatorTypeEnum.Mul,
            "\\" or "/" => OperatorTypeEnum.Div,
            _ => throw new InvalidEnumArgumentException($"Unrecognized operator '{character}'.")
        };
    }
}