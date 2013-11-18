namespace StringCalculator
{
    using System;
    using System.Linq;

    public class Calculator
    {
        private readonly IConsole _console;

        private readonly ITransactions _transactions;

        private readonly IUser _user;

        public Calculator(IConsole console, ITransactions transactions, IUser user)
        {
            _console = console;
            _transactions = transactions;
            _user = user;
        }

        public int Add(string input)
        {
            if (input.Contains("-"))
            {
                throw new InvalidOperationException();
            }

            var parts = input.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries);

            var sum = parts.Sum(p => int.Parse(p));

            _console.WriteLine(sum);
            _transactions.Record(input, sum);
            _console.WriteLine("You owe us money!");
            _console.WriteLine("Hello, " + _user.Name );

            return sum;

        }

        public interface ITransactions
        {
            void Record(string input, int sum);
        }

        public interface IConsole
        {
            void WriteLine(object o);
        }

        private static void Main(string[] args)
        {
            //var calc = new Calculator();
            //var result = calc.Add(string.Empty);
            //Console.WriteLine(result);

            //var myStrings = new[] {"3", "5", "1"};
            //var result = calc.Add(myStrings);
            //Console.WriteLine(result);
        }
    }

    public interface IUser
    {
        string Name
        {
            get;
            set;
        }
    }
}