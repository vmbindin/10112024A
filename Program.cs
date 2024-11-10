using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.Linq;

namespace MathApp
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Create a root command with a description
            var rootCommand = new RootCommand
            {
                Description = "Math app for performing various calculations"
            };

            // Define the sum command
            var sumCommand = new Command("sum", "Sums the provided numbers");
            sumCommand.AddAlias("+");

            // Define the sum arguments 
            var sumArgument = new Argument<int[]>
                (name: "numbers",
                description: "The numbers to sum.",
                getDefaultValue: () => [0]);

            sumCommand.Add(sumArgument);

            sumCommand.SetHandler((numbers) =>
            {
                if (numbers == null || numbers.Length == 0)
                {
                    Console.WriteLine("No numbers provided.");
                }
                else
                {
                    int sum = numbers.Sum();
                    Console.WriteLine($"Sum: {sum}");
                }
            }, sumArgument);

            // Define the subtract command
            var subCommand = new Command("subtract", "Subtracts the provided numbers");
            subCommand.AddAlias("-");

            // Define the subtract arguments 
            var subArgument = new Argument<int[]>
                (name: "numbers",
                description: "The numbers to subtract.",
                getDefaultValue: () => [0]);

            subCommand.Add(subArgument);

            subCommand.SetHandler((numbers) =>
            {
                if (numbers == null || numbers.Length == 0)
                {
                    Console.WriteLine("No numbers provided.");
                }
                else
                {
                    int sub = numbers.Aggregate((acc, x) => acc - x);
                    Console.WriteLine($"Subtract: {sub}");
                }
            }, subArgument);

            // Define the multiply command
            var multiplyCommand = new Command("multiply", "Multiplies the provided numbers");
            multiplyCommand.AddAlias("x");

            // Define the multiply arguments 
            var multiplyArgument = new Argument<int[]>
                (name: "numbers",
                description: "The numbers to multiply.",
                getDefaultValue: () => [0]);

            multiplyCommand.Add(multiplyArgument);
            multiplyCommand.SetHandler((numbers) =>
            {
                if (numbers == null || numbers.Length == 0)
                {
                    Console.WriteLine("No numbers provided.");
                }
                else
                {
                    int product = numbers.Aggregate(1, (acc, x) => acc * x);
                    Console.WriteLine($"Product: {product}");
                }
            }, multiplyArgument);

            // Define the divide command
            var divideCommand = new Command("divide", "Divide the provided numbers");
            divideCommand.AddAlias("d"); // issue using '/' as it is used to represent options

            // Define the divide arguments 
            var divideArgument = new Argument<double[]>
                (name: "numbers",
                description: "The numbers to divide.",
                getDefaultValue: () => [0]);

            divideCommand.Add(divideArgument);
            divideCommand.SetHandler((numbers) =>
            {
                if (numbers == null || numbers.Length == 0)
                {
                    Console.WriteLine("No numbers provided.");
                }
                else
                {
                    double divide = numbers.Aggregate((acc, x) => acc / x);
                    Console.WriteLine($"Divide: {divide}");
                }
            }, divideArgument);

            // Add the subcommands to the root command
            rootCommand.AddCommand(sumCommand);
            rootCommand.AddCommand(subCommand);
            rootCommand.AddCommand(multiplyCommand);
            rootCommand.AddCommand(divideCommand);

            // Check if no arguments are provided and display help
            if (args.Length == 0)
            {
                rootCommand.Invoke("-h");
                return 0;
            }

            // Invoke the root command
            return await rootCommand.InvokeAsync(args);
        }
    }
}