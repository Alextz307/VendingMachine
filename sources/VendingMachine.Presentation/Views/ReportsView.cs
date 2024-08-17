using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;

namespace Nagarro.VendingMachine.Presentation.Views
{
    public class ReportsView : DisplayBase, IReportsView
    {
        private bool ParseDate(string dateType, out DateTime? date, DateTime? lowerBound)
        {
            string dateFormat = "yyyy-MM-dd";

            while (true)
            {
                Display($"\nEnter the {dateType} date for the report in the format {dateFormat} " +
                     "or type 'cancel' if you want to cancel the operation: ", ConsoleColor.Cyan);

                string? input = Console.ReadLine();

                if (input == "cancel")
                {
                    date = null;
                    return false;
                }

                if (!DateTime.TryParseExact(input, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    Display("You did not cancel or enter a valid date in the specified format!\n", ConsoleColor.Red);
                    continue;
                }

                if (parsedDate < lowerBound)
                {
                    Display("The ending date must be greater or equal than the starting one!\n", ConsoleColor.Red);
                    continue;
                }

                date = parsedDate;
                return true;
            }
        }

        public bool AskForTimeInterval(out DateTime? startTime, out DateTime? endTime)
        { 
            if (!ParseDate("starting", out startTime, DateTime.MinValue))
            {
                startTime = endTime = null;
                return false;
            }

            if (!ParseDate("ending", out endTime, startTime))
            {
                startTime = endTime = null;
                return false;
            }

            return true;
        }

        public void DisplayCanceledMessage()
        {
            Display("\nThe operation has been canceled!", ConsoleColor.Red);
        }

        public void DisplaySuccessMessage()
        {
            Display("\nThe requested report has been created!", ConsoleColor.Green);
        }
    }
}
