using System;
using System.Globalization;
namespace LoopBreakers.Logic.Validators
{
    public class InputValidators
    {
        public static (string outputText, bool isValid) GetText(int minLenght, int maxLenght, string inputText)
        {
            string outputText = "";
            bool isValid = false;
            inputText = inputText.Trim();
            if (inputText.Length < minLenght || inputText.Length > maxLenght)
            {
                //Console.Write($"Wrong value (min: {minLenght}, max: {maxLenght} sign). Type again: ");
                // GetText(minLenght, maxLenght);
                outputText = inputText;
                isValid = true;
            }
            return (outputText, isValid);
        }

        public static void ChoosedTimePeriod(int userTransferPeriodOption, out DateTime startPeriod, out DateTime endPeriod)
        {
            if (userTransferPeriodOption == 1)
            {
                startPeriod = DateTime.Now.AddDays(-30);
                endPeriod = DateTime.Now;
            }
            else if (userTransferPeriodOption == 2)
            {
                startPeriod = DateTime.Now.AddDays(-90);
                endPeriod = DateTime.Now;
            }
            else if (userTransferPeriodOption == 3)
            {
                startPeriod = DateTime.Now.AddDays(-180);
                endPeriod = DateTime.Now;
            }
            else
            {
                startPeriod = DateTime.Now.AddDays(1);
                endPeriod = DateTime.Now.AddDays(1);
            }
        }
        public static void ChoosedTimePeriodCustomed(string customedStartDate, string customedEndDate, out DateTime startPeriod, out DateTime endPeriod)
        {
            var cultureInfo = new CultureInfo("pl-PL");

            try
            {
                startPeriod = Convert.ToDateTime(customedStartDate, cultureInfo);
                if (startPeriod > DateTime.Now)
                {
                    startPeriod = DateTime.Now;
                }
                endPeriod = Convert.ToDateTime(customedEndDate, cultureInfo);
            }
            catch
            {
                startPeriod = DateTime.Now.AddDays(1);
                endPeriod = DateTime.Now.AddDays(1);
            }
        }
    }
}


