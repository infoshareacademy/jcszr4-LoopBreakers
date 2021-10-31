using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopBreakers.Logic.Validators
{
    class InputValidators
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
    }
}
