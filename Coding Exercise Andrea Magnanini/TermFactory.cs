namespace Coding_Exercise_Andrea_Magnanini
{
    using Extensions;
    using Terms;

    public class TermFactory : ITermFactory
    {
        public ITerm CreateTerm(string match)
        {
            return double.TryParse(match, out var value)
                ? new SignedNumber(value)
                : new Operator(match.ToOperatorType());
        }
    }
}