﻿namespace Nagarro.VendingMachine.Presentation
{
    public class DisplayBase
    {
        protected void DisplayLine(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        protected void Display(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = oldColor;
        }

        public void DisplayError(string errorMessage)
        {
            Display(errorMessage, ConsoleColor.Red);
        }
    }
}