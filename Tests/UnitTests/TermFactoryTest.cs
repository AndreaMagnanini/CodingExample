namespace Tests.UnitTests
{
    using Coding_Exercise_Andrea_Magnanini.Factories;
    using Coding_Exercise_Andrea_Magnanini.Extensions;
    using Coding_Exercise_Andrea_Magnanini.Terms;
    using NUnit.Framework;

    [TestFixture]
    public class TermFactoryTest
    {
        [Test]
        [TestCase("3.0")]
        [TestCase("3,33")]
        [TestCase("3.1415")]
        public void CreateTerm_WhenTermIsUnsignedNumber_ReturnsUnsignedNumber(string term)
        {
            // Given
            var factory = new TermFactory();

            // When
            var result = factory.CreateTerm(term);

            // Then
            Assert.That(result, Is.InstanceOf<ISignedNumber>());
            Assert.That((result as SignedNumber)!.Value, Is.EqualTo(double.Parse(term)));
        }

        [Test]
        [TestCase("-3.0")]
        [TestCase("+3,33")]
        [TestCase("-3.1415")]
        public void CreateTerm_WhenTermIsSignedNumber_ReturnsSignedNumber(string term)
        {
            // Given
            var factory = new TermFactory();

            // When
            var result = factory.CreateTerm(term);

            // Then
            Assert.That(result, Is.InstanceOf<ISignedNumber>());
            Assert.That((result as SignedNumber)!.Value, Is.EqualTo(double.Parse(term)));
        }

        [Test]
        [TestCase("-")]
        [TestCase("+")]
        [TestCase("*")]
        [TestCase("x")]
        [TestCase("X")]
        [TestCase("/")]
        [TestCase("\\")]
        public void CreateTerm_WhenTermIsSOperator_ReturnsOperator(string term)
        {
            // Given
            var factory = new TermFactory();

            // When
            var result = factory.CreateTerm(term);

            // Then
            Assert.That(result, Is.InstanceOf<IOperator>());
            Assert.That((result as Operator)!.Type, Is.EqualTo(term.ToOperatorType()));
        }
    }
}
