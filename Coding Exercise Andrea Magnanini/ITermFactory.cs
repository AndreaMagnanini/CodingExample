namespace Coding_Exercise_Andrea_Magnanini
{
    using Terms;

    public interface ITermFactory
    {
        ITerm CreateTerm(string match);
    }
}