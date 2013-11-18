using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Should;

namespace StringCalculator.UnitTests.Core
{
    using Moq;

    using SpecsFor;

    public class CalculatorSpecs
    {
        
        public class when_adding_an_empty_string : SpecsFor<Calculator>
        {
            private int _result;

            protected override void Given()
            {
                GetMockFor<IUser>()
                    .SetupGet(u => u.Name)
                    .Returns("Sean");
            }

            protected override void When()
            {
                _result = SUT.Add(string.Empty);
            }

            [Test]
            public void then_it_should_return_zero()
            {
                _result.ShouldEqual(0);
            }

            [Test]
            public void then_it_should_write_zero_to_the_console()
            {
                GetMockFor<Calculator.IConsole>()
                    .Verify(c => c.WriteLine(0));
            }

            [Test]
            public void then_it_logs_a_transaction()
            {
                GetMockFor<Calculator.ITransactions>()
                    .Verify(t => t.Record(string.Empty, 0));
            }

            [Test]
            public void then_it_should_tell_the_user_hello()
            {
                GetMockFor<Calculator.IConsole>()
                    .Verify(c => c.WriteLine("Hello, Sean"));
            }
        }

        public class when_adding_more_than_one_string : SpecsFor<Calculator>
        {
            private int _result;

            protected override void Given()
            {
                GetMockFor<IUser>()
                    .Setup(u => u.Name)
                    .Returns("Sean");
            }

            protected override void When()
            {
                const string testInput = "2,3,5";
                _result = SUT.Add(testInput);
            }

            [Test]
            public void then_it_should_return_a_valid_result()
            {
                _result.ShouldEqual(10);
            }
        }
    }

    /*
    [TestFixture]
    public class CalculatorSpecs
    {
        private Calculator SUT;

        private Mock<Calculator.IConsole> MockConsole;

        private Mock<Calculator.ITransactions> MockTransactions;

        [SetUp]
        public void CreateCalculator()
        {
            MockTransactions = new Mock<Calculator.ITransactions>();
            MockConsole = new Mock<Calculator.IConsole>();
            SUT = new Calculator(MockConsole.Object, MockTransactions.Object);
        }

        [Test]
        public void Adding_an_empty_string_returns_zero()
        {
            var sum = SUT.Add(string.Empty);

            sum.ShouldEqual(0);
        }

        [Test]
        public void Adding_one_returns_one()
        {
            var sum = SUT.Add("1");

            sum.ShouldEqual(1);
        }

        [Test]
        public void Adding_two_returns_two()
        {
            var sum = SUT.Add("2");

            sum.ShouldEqual(2);
        }

        [Test]
        public void Adding_one_and_two_returns_three()
        {
            var sum = SUT.Add("1,2");

            sum.ShouldEqual(3);
        }

        [Test]
        public void Adding_one_two_three_returns_six()
        {
            var sum = SUT.Add("1,2,3");

            sum.ShouldEqual(6);
        }

        [Test]
        public void Adding_negative_number_throws_exception()
        {
            Assert.Throws<InvalidOperationException>(() => SUT.Add("-1"));
        }

        [Test]
        public void Adding_zero_writes_zero_to_the_console()
        {
            var sum = SUT.Add("0");

            MockConsole.Verify(c => c.WriteLine(0));
        }

        [Test]
        public void Adding_numbers_records_the_input_and_output()
        {
            var sum = SUT.Add("1,3");

            MockTransactions.Verify(t => t.Record("1,3", 4));
        }

        [Test]
        public void Adding_numbers_writes_message_to_the_console()
        {
            SUT.Add("1,2,3");

            MockConsole.Verify(c => c.WriteLine("You owe us money!"));
        }
    }
     */
}
