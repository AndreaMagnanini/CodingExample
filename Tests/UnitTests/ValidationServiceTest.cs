namespace Tests.UnitTests
{
    using Coding_Exercise_Andrea_Magnanini;
    using Coding_Exercise_Andrea_Magnanini.Extensions;
    using Coding_Exercise_Andrea_Magnanini.Terms;
    using Moq;
    using NUnit.Framework;
    using System.Data;

    [TestFixture]
    public class ValidationServiceTest
    {
        [Test]
        [TestCase("AA+BB")]
        [TestCase("AA + BB")]
        [TestCase("ABC")]
        [TestCase("33 + AA")]
        [TestCase("AA + 33")]
        [TestCase("-33*-4R")]
        public void BuildExpression_WhenExpressionIsInvalid_ThrowsException(string input)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            var service = new ValidationService(termFactory.Object);
            
            // Then
            Assert.Throws<InvalidExpressionException>(() => service.BuildExpression(input));
        }

        [Test]
        [TestCase("-2+44", "-2", "44", "+")]
        [TestCase("-2*-33", "-2", "-33", "*")]
        [TestCase("15*-3", "15", "-3", "*")]
        [TestCase("15/-3", "15", "-3", "/")]
        [TestCase("15\\-3", "15", "-3", "\\")]
        [TestCase("-6-3", "-6", "3", "-")]
        public void BuildExpression_WhenExpressionContainsSignedNumber_ReturnsExpression(string input, string firstTerm, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(firstTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(firstTerm)) < double.Epsilon))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(3));
            Mock.Verify();
        }

        [Test]
        [TestCase("2+44", "2", "44", "+")]
        [TestCase("2*33", "2", "33", "*")]
        [TestCase("15x3", "15", "3", "x")]
        [TestCase("15X3", "15", "3", "X")]
        [TestCase("15*3", "15", "3", "*")]
        [TestCase("15/3", "15", "3", "/")]
        [TestCase("15\\3", "15", "3", "\\")]
        [TestCase("6-3", "6", "3", "-")]
        public void BuildExpression_WhenExpressionContainsUnsignedNumber_ReturnsExpression(string input, string firstTerm, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(firstTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(firstTerm)) < double.Epsilon))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(3)); ;
            Mock.Verify();
        }

        [Test]
        [TestCase("-2 + 44", "-2", "44", "+")]
        [TestCase(" -2 *-33", "-2", "-33", "*")]
        [TestCase(" -2 x-33", "-2", "-33", "x")]
        [TestCase(" -2 X-33", "-2", "-33", "X")]
        [TestCase("15* -3", "15", "-3", "*")]
        [TestCase("15/ -3 ", "15", "-3", "/")]
        [TestCase("15\\ -3 ", "15", "-3", "\\")]
        [TestCase(" 15 / -3 ", "15", "-3", "/")]
        [TestCase(" 15 \\ -3 ", "15", "-3", "\\")]
        [TestCase(" - 6 - 3 ", "-6", "3", "-")]
        [TestCase(" - 6 -3 ", "-6", "3", "-")]
        [TestCase(" -6 -3 ", "-6", "3", "-")]
        public void BuildExpression_WhenExpressionContainsSpaces_ReturnsExpression(string input, string firstTerm, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(firstTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(firstTerm)) < double.Epsilon))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(3));
            Mock.Verify();
        }

        [Test]
        [TestCase("-2,0+44", "-2,0", "44", "+")]
        [TestCase("-2,0*-33,0", "-2,0", "-33,0", "*")]
        [TestCase("-2,0x-33,0", "-2,0", "-33,0", "x")]
        [TestCase("-2,0X-33,0", "-2,0", "-33,0", "X")]
        [TestCase("15,5*-3", "15,5", "-3", "*")]
        [TestCase("15,5x-3", "15,5", "-3", "x")]
        [TestCase("15,5X-3", "15,5", "-3", "X")]
        [TestCase("15/-3,5", "15", "-3,5", "/")]
        [TestCase("15\\-3,5", "15", "-3,5", "\\")]
        [TestCase("-6,2525 -3,9", "-6,2525", "3,9", "-")]
        public void BuildExpression_WhenExpressionContainsDecimalsWithCommas_ReturnsExpression(string input, string firstTerm, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(firstTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(firstTerm)) < double.Epsilon))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(3));
            Mock.Verify();
        }

        [Test]
        [TestCase("-2.0+44", "-2.0", "44", "+")]
        [TestCase("-2.0*-33.0", "-2.0", "-33.0", "*")]
        [TestCase("-2.0x-33.0", "-2.0", "-33.0", "x")]
        [TestCase("-2.0X-33.0", "-2.0", "-33.0", "X")]
        [TestCase("15.5X-3", "15.5", "-3", "X")]
        [TestCase("15.5x-3", "15.5", "-3", "x")]
        [TestCase("15.5*-3", "15.5", "-3", "*")]
        [TestCase("15/-3.5", "15", "-3.5", "/")]
        [TestCase("15\\-3.5", "15", "-3.5", "\\")]
        [TestCase("-6.2525 -3.9", "-6.2525", "3.9", "-")]
        public void BuildExpression_WhenExpressionContainsDecimalsWithDots_ReturnsExpression(string input, string firstTerm, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(firstTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(firstTerm)) < double.Epsilon))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(3));
            Mock.Verify();
        }

        [Test]
        [TestCase("+44", "44", "+")]
        [TestCase("-4", "4", "-")]
        [TestCase("*-33.0",  "-33.0", "*")]
        [TestCase("x-33.0", "-33.0", "x")]
        [TestCase("X-33.0", "-33.0", "X")]
        [TestCase("*-3", "-3", "*")]
        [TestCase("x-3", "-3", "x")]
        [TestCase("X-3", "-3", "X")]
        [TestCase("/-3.5",  "-3.5", "/")]
        [TestCase("\\-3.5", "-3.5", "\\")]
        [TestCase("-3,9", "3,9", "-")]
        public void BuildExpression_WhenExpressionHasJustTwoTerms_ReturnsExpression(string input, string secondTerm, string operation)
        {
            // Given
            var termFactory = new Mock<ITermFactory>(MockBehavior.Strict);
            termFactory.Setup(tf => tf.CreateTerm(operation))
                .Returns(It.Is<IOperator>(o => o.Type == operation.ToOperatorType()))
                .Verifiable();
            termFactory.Setup(tf => tf.CreateTerm(secondTerm))
                .Returns(It.Is<SignedNumber>(s => Math.Abs(s.Value - double.Parse(secondTerm)) < double.Epsilon))
                .Verifiable();
            var service = new ValidationService(termFactory.Object);

            // When
            var expression = service.BuildExpression(input);

            // Then
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Count, Is.EqualTo(2));
            Mock.Verify();
        }
    }
}