using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaspichanNumbers
{
    class Program
    {
        // Class variables
        private static uint numBase = 10;
        private static List<string> numbers;
        private static bool testing = false;

        // Properties
        public static uint NumBase 
        { 
            get
            {
                return numBase;
            }
            set
            {
                numBase = value;
            }
        }

        public static List<string> Numbers 
        { 
            get
            {
                return numbers;
            }
            set
            {
                numbers = value;
            }
        }
        public static bool Testing
        {
            get
            {
                return testing;
            }
            set
            {
                testing = value;
            }
        }


        // Methods


        // Main Method
        static void Main(string[] args)
        {
            // IsTesting
            Testing = false;

            // Number Generator
            NumBase = 256;
            GenerateNumbers();
            // PrintGeneratedNumbers();

            // User input
            ulong numberToConvert = UserInput();
            

            // Convert Number
            string convertedNumber = ConvertNumber(numberToConvert);
            // Print Number
            PrintNumber(convertedNumber);

        }

        

        


        


        // Number Generator Methods
        private static void GenerateNumbers()
        {
            Numbers = new List<string>();
 	        for (int num = 0; num < NumBase; num++)
			{
                string currentNumber = GetNumber(num);
                // PrintMessage(currentNumber);
                Numbers.Add(currentNumber);
			}
        }

        

        private static string GetNumber(int num)
        {
            int firstNumID = (int)((num / 26) - 1);
            string firstNum = "";
            if (firstNumID < 0)
	        {
		        firstNum = "";
	        }
            else
            {
                firstNum = ((char)(((uint)'a') + firstNumID)).ToString();
            }

            uint secondNumID = (uint)(num % 26);
            string secondNum = "";
            if (secondNumID < 0)
	        {
		        secondNum = "";
	        }
            else
            {
                secondNum = ((char)(((uint)'A') + secondNumID)).ToString();
            }
            string currentNumber = firstNum + secondNum;
 	        return currentNumber;
        }


        // User Input
        private static ulong UserInput()
        {
            return ulong.Parse(Console.ReadLine());
        }


        // Number Conversion
        private static string ConvertNumber(ulong numberToConvert)
        {
            string convertedNumber = "";
            PrintMessage("Foo!");
            PrintMessage(numberToConvert.ToString());
            PrintMessage((numberToConvert / NumBase).ToString());
            PrintMessage((numberToConvert % NumBase).ToString());

            if (numberToConvert == 0)
            {
                return "A";
            }

            while (numberToConvert / NumBase != 0 || numberToConvert % NumBase != 0)
            {
                uint numberID = (uint)(numberToConvert % NumBase);
                numberToConvert /= NumBase;
                PrintMessage("Inside While!");
                convertedNumber = Numbers[(int)numberID] + convertedNumber;
                PrintMessage(convertedNumber);
            }

            PrintMessage("Bar");

            return convertedNumber;
        }

        
        // Print Methods
        private static void PrintNumber(string convertedNumber)
        {
 	        Console.WriteLine(convertedNumber);
        }

        private static void PrintGeneratedNumbers()
        {
            // If we're not debuging - don't print anything
            if (!Testing)
            {
                return;
            }


            foreach (string num in Numbers)
	        {
		        Console.Write("{0}, ", num);
	        }
        }

        private static void PrintMessage(string currentNumber)
        {
            // If we're not debuging - don't print anything
            if (!Testing)
            {
                return;
            }


            Console.WriteLine(currentNumber);
        }
    }
}
