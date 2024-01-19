namespace Coding_Exercise_Andrea_Magnanini.Terms
{
    public class SignedNumber : Term, ISignedNumber
    {
        public SignedNumber(double value)
        {
            this.Value = value;
        }

        public double Value { get; }
    }
}