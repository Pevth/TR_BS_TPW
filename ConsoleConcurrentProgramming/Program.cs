// See https://aka.ms/new-console-template for more information
ConcurrentProgramming.FirstProgram firstProgram = new ConcurrentProgramming.FirstProgram(5, 6);
Console.WriteLine(firstProgram.HelloWorld());
Console.WriteLine($"{firstProgram.A}+{firstProgram.B}={firstProgram.Add()}");
