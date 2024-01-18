namespace Coding_Exercise_Andrea_Magnanini.Terms
{
    using Enums;

    public interface IOperator : ITerm
    {
        OperatorTypeEnum Type { get; }
    }
}