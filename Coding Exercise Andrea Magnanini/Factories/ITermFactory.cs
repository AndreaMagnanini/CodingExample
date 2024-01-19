namespace Coding_Exercise_Andrea_Magnanini.Factories
{
    using Terms;

    public interface ITermFactory
    {
        ITerm CreateTerm(string match);
    }
}