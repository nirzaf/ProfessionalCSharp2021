﻿using System;

using MulticastDelegates;

Action<double> operations = MathOperations.MultiplyByTwo;
operations += MathOperations.Square;

ProcessAndDisplayNumber(operations, 2.0);
ProcessAndDisplayNumber(operations, 7.94);
ProcessAndDisplayNumber(operations, 1.414);
Console.WriteLine();


void ProcessAndDisplayNumber(Action<double> action, double value)
{
    Console.WriteLine();
    Console.WriteLine($"ProcessAndDisplayNumber called with value = {value}");
    action(value);
}
