namespace Coding_Exercise_Andrea_Magnanini
{
    using System.Data;
    using System.Text.RegularExpressions;
    using Terms;

    public class ValidationService
    {
        private const string TermExtractionPattern = @"^\s*([+-]?\d+[.,]?\d*)?\s*([-+xX*/\\])\s*([+-]?\d+[,.]?\d*)\s*$";
        private readonly ITermFactory termFactory;

        public ValidationService(ITermFactory termFactory)
        {
            this.termFactory = termFactory;
        }
        
        public IList<ITerm> BuildExpression(string input)
        {
            var correctInput = input.Replace(" ", string.Empty);
            if (!Regex.IsMatch(correctInput, TermExtractionPattern))
            {
                throw new InvalidExpressionException("Invalid Expression.");
            }

            var matches = Regex.Matches(correctInput, TermExtractionPattern)
                .First().Groups.Values
                .ToList();
            return matches
                .Skip(1)
                .Where(m => !string.IsNullOrEmpty(m.Value))
                .Select(group => this.termFactory.CreateTerm(group.Value))
                .ToList();
        }
    }
}
