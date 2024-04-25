// See https://aka.ms/new-console-template for more information
// ReSharper disable file EnforceIfStatementBraces

Console.WriteLine("checksum calculator for german ID card numbers");

//GET INPUT FROM COMMAND LINE ARGUMENTS
var input = args[0];
// var input = "123456789";

Console.WriteLine($"input: {input}");

// VALIDATE INPUT
if (
    // check if input is 9 characters
    input.Length != 9 ||
    // check if characters are alphanumeric
    input.ToList()
        .Select(char.IsLetterOrDigit)
        .Any(b => !b) ||
    // check if there are invalid characters
    input.ToList()
        .Select(c => "abdeioqsu".Contains(c.ToString().ToLower()[0]))
        .Any(b => b))
{
    // return error if input is invalid
    Console.WriteLine("error: string must be 9 valid alphanumeric characters");
    return;
}

// CALCULATE CHECKSUM
var result = input

    // convert letters to numbers
    .Select(c =>
    {
        if (char.IsNumber(c))
        {
            Console.WriteLine($"{c}");
            return int.Parse([c]);
        }

        var number = c - 55;
        Console.WriteLine($"{c} to {number}");
        return number;

    })

    // convert list into 3 arrays of 3 ints
    .Chunk(3)

    // in each subgroup, multiply each with weight and then sum them up
    .Select(group =>
    {
        var subSum =
            group[0] * 7 % 10 +
            group[1] * 3 % 10 +
            group[2] * 1 % 10;
        return subSum;
    })

    // get last digit of the sum
    .Sum();

// OUTPUT RESULT
Console.WriteLine($"result: {result}");