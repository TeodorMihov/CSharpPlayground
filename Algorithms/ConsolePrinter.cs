using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class ConsolePrinter
    {
        public static void PrintCondition(string text)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
        } 

        public static void PrintSolution(string text)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(text);
        }

        public static void PrintSolution(string text, IEnumerable<int> collection)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(text + string.Join(", ", collection));
        }
            
    }
}