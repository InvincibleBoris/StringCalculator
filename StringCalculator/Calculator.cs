namespace StringCalculator
{
    using System;
    using System.Linq;
    using System.Security.AccessControl;

    using StringCalculator.Log4Net;
    using StringCalculator.LoggingInterface;

    public class Calculator
    {
        private readonly IConsole _console;

        private readonly ITransactions _transactions;

        private readonly IUser _user;

        private ILogger _logger;

        public bool wroteToLog;

        // Passing interfaces through Calculator constructor so that when Unit Tests create the Calculator SUT, we can verify that methods are invoked etc...
        public Calculator(IConsole console, ITransactions transactions, IUser user, ILogger logger)
        {
            _console = console;
            _transactions = transactions;
            _user = user;
            _logger = logger;
        }

        public int Add(string input)
        {
            wroteToLog = false;

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

            string message = "We calculated the sum to be: " + sum;
            WriteToLog("General", LogLevel.FATAL, message);

            return sum;
        }

        public void WriteToLog(string category, LogLevel level, string message)
        {
            try
            {
                _logger = LoggerFactory.GetLogger();
                _logger.WriteMessage(category, level, message);
                wroteToLog = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failed to write to the log: {0}", exception.Message);
            }
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
            ConsoleTest console = new ConsoleTest();
            TransactionsTest transactions = new TransactionsTest();
            LoggerTest loggertest = new LoggerTest();
            UserTest user = new UserTest();

            var calc = new Calculator(console, transactions, user, loggertest);
            calc.Add("44,2");
        }
    }

    public class ConsoleTest : Calculator.IConsole
    {
        public void WriteLine(object o)
        {
            Console.WriteLine(o.ToString());
        }
    }

    public class TransactionsTest : Calculator.ITransactions
    {
        public void Record(string input, int sum)
        {
            // Record the transaction here...
        }
    }

    public class UserTest : IUser
    {
        public string Name
        {
            get;
            set;
        }
    }

    public class LoggerTest : ILogger
    {
        public void WriteMessage(string category, LogLevel level, string message)
        {
            // Do some logging here...
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