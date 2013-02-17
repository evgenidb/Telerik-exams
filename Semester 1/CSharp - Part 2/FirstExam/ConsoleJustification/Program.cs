using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJustification
{
    class Program
    {
        // Class Variables
        private static bool testing = false;

        // Properties
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




            if (!Testing)
            {
                // User input
                int userInputLineCount = 0;
                int maxLineWidth = 0;
                List<string> lines = UserInputContainer(ref userInputLineCount, ref maxLineWidth);

                

                // Justificate
                List<string> justifiedLines = Justify(lines, maxLineWidth);


                // Print Text
                PrintText(justifiedLines);
            }
            else
            {
                RunTests();
            }
            


            

        }

        



        // User Input
        private static List<string> UserInputContainer(ref int userInputLineCount, ref int maxLineWidth)
        {
            userInputLineCount = ReadUserLineCount();
            maxLineWidth = ReadLineWidth();

            List<string> lines = ReadLines(userInputLineCount);
            
            
            return lines;
        }

        private static List<string> ReadLines(int userInputLineCount)
        {
            List<string> lines = new List<string>();
            for (int line = 0; line < userInputLineCount; line++)
            {
                string strLine = Console.ReadLine();
                lines.Add(strLine);
            }

            return lines;
        }

        private static int ReadUserLineCount()
        {
            return int.Parse(Console.ReadLine());
        }

        private static int ReadLineWidth()
        {
            return int.Parse(Console.ReadLine());
        }



        // Justify
        static List<string> Justify(List<string> textToJustify, int maxLineWidth)
        {
            List<string> justifiedText = new List<string>();

            //PrintDebugMessage("Justify Method - Start");

            // Trim the lines of the extra spaces on both ends
            TrimWhitespaces(textToJustify);

            // Lines to single string

            string text = LineTextToString(textToJustify);

            //PrintDebugMessage(text);

            // Replace all multiple spaces with single space
            string[] words = TextToWords(text);

            PrintDebugMessageArray(words);

            // Construct the New Lines
            List<string> newLines = ConstructNewLines(words, maxLineWidth);
            // PrintDebugMessageList(newLines);

            // Justify the New Lines
            justifiedText = JustifyText(newLines, maxLineWidth);

            //PrintDebugMessage("Justify Method - End");

            return justifiedText;
        }

        

        

        private static void TrimWhitespaces(List<string> textToJustify)
        {
            for (int line = 0; line < textToJustify.Count; line++)
            {
                textToJustify[line].Trim();
            }
        }

        private static List<string> JustifyText(List<string> newLines, int maxLineWidth)
        {
            List<string> justifiedText = new List<string>();

            

            foreach (string line in newLines)
            {
                if (line.Length >= maxLineWidth || !(line.Contains(" ")))
                {
                    //PrintDebugMessage("One-liner!");
                    

                    // The line is with maxWidth or there is just one word, i.e. no need to justify
                    // so we need only to add it to the justifiedText
                    justifiedText.Add(line);

                    // Continue to the next line
                    continue;
                }
                else
                {
                    //PrintDebugMessage("Multy-liner!");


                    int spacesToAdd = maxLineWidth - line.Length;
                    StringBuilder lineToJustify = new StringBuilder(line);
                    string spacesToSearch = " ";
                    int currentIndexOfSpaces = -1;

                    for (int space = 0; space < spacesToAdd; space++)
                    {
                        currentIndexOfSpaces = (lineToJustify.ToString()).IndexOf(spacesToSearch, (currentIndexOfSpaces + 1));

                        if (spacesToSearch == "   ")
                        {
                            PrintDebugMessage("");
                        }

                        if (currentIndexOfSpaces < 0)
	                    {
		                    spacesToSearch += " ";
                            currentIndexOfSpaces = -1;
                            space--;
                            continue;
	                    }

                        lineToJustify.Insert(index: currentIndexOfSpaces, value: " ");
                        currentIndexOfSpaces += spacesToSearch.Length;
                    }

                    // Add the justified line
                    justifiedText.Add(lineToJustify.ToString());
                }
            }

            return justifiedText;
        }

        private static List<string> ConstructNewLines(string[] words, int maxLineWidth)
        {
            List<string> newLines = new List<string>();

            StringBuilder currentLine = new StringBuilder();
            for (int wordIndex = 0; wordIndex < words.Length; wordIndex++ )
            {
                if ((currentLine.Length + 1 + words[wordIndex].Length) <= maxLineWidth)          // Current Line Width + 1 (for the space) + Current Word Width must be <= to Max Line Width
                {
                    if (currentLine.Length > 0)
                    {
                        // Add a space if there is a word before the current one
                        currentLine.Append(" ");
                    }
                }
                else
                {
                    // Add the new constructed line
                    if (currentLine.Length > 0)
                    {
                        newLines.Add(currentLine.ToString());
                    }


                    // Clear the current line for the next line
                    currentLine.Clear();
                }

                // Append the current word
                currentLine.Append(words[wordIndex]);

                // If it's the last word - Append it!
                if (wordIndex + 1 == words.Length)
                {
                    newLines.Add(currentLine.ToString());
                }
            }

            return newLines;
        }

        private static string[] TextToWords(string text)
        {
            char[] separators = { ' ' };
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string LineTextToString(List<string> textToJustify)
        {
            StringBuilder text = new StringBuilder();

            for (int lineNumber = 0; lineNumber < textToJustify.Count; lineNumber++)
            {
                text.Append(textToJustify[lineNumber]);

                if (lineNumber + 1 != textToJustify.Count)
                {
                    // i.e. it's not the last line - add an extra space
                    text.Append(" ");
                }
            }

            return text.ToString();
        }

        



        // Prints
        private static void PrintText(List<string> text)
        {
            PrintDebugMessage("");
            foreach (string line in text)
            {
                Console.WriteLine("{0}", line);
            }

            if (Testing)
            {
                Console.WriteLine();
                Console.WriteLine("Line Count: {0}", text.Count);
                Console.WriteLine();
            }
            
        }


        private static void PrintDebugMessage(string msg = "")
        {
            // If we're not debuging - don't print anything
            if (!Testing)
            {
                return;
            }


            Console.WriteLine(msg);
        }

        private static void PrintDebugMessageArray(string[] words)
        {
            // If we're not debuging - don't print anything
            if (!Testing)
            {
                return;
            }

            foreach (string w in words)
            {
                Console.WriteLine(w);
            }
        }

        private static void PrintDebugMessageList(List<string> lines)
        {
            // If we're not debuging - don't print anything
            if (!Testing)
            {
                return;
            }

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }


        // Testing

        // Test Container
        private static void RunTests()
        {
            int failures = 0;

            Func<bool>[] tests = { Test1, Test2, Test3, Test4, Test5 };

            for (int i = 0; i < tests.Length; i++)
            {
                if (!tests[i]())
                {
                    failures++;
                    Console.WriteLine();
                    PrintDebugMessage(String.Format("Test {0} Failed!", i + 1));
                    Console.WriteLine("____________________________");
                    Console.WriteLine();
                }
            }

            PrintDebugMessage(String.Format("{0} of {1} Tests Failed!", failures, tests.Length));
        }

        // Tests
        private static bool Test1()
        {
            int maxLineWidth = 20;

            List<string> lines = new List<string>();
            lines.Add("We happy few      we band");
            lines.Add("of brothers for he who sheds");
            lines.Add("his blood");
            lines.Add("with");
            lines.Add("me shall be my brother");

            List<string> justifiedLines = Justify(lines, maxLineWidth);
            PrintText(justifiedLines);

            bool isSuccess = false;

            return isSuccess;
        }

        private static bool Test2()
        {
            int maxLineWidth = 18;

            List<string> lines = new List<string>();
            lines.Add("Beer beer beer Im going for");
            lines.Add("    a   ");
            lines.Add("beer");
            lines.Add("Beer beer beer Im gonna");
            lines.Add("drink some beer");
            lines.Add("I love drinkiiiiiiiiing");
            lines.Add("beer");
            lines.Add("lovely");
            lines.Add("lovely");
            lines.Add("beer");

            List<string> justifiedLines = Justify(lines, maxLineWidth);
            PrintText(justifiedLines);


            bool isSuccess = false;

            return isSuccess;
        }


        private static bool Test3()
        {
            int maxLineWidth = 1;

            List<string> lines = new List<string>();
            lines.Add("a");

            List<string> justifiedLines = Justify(lines, maxLineWidth);
            PrintText(justifiedLines);


            bool isSuccess = false;

            return isSuccess;
        }


        private static bool Test4()
        {
            int maxLineWidth = 20;

            List<string> lines = new List<string>();
            lines.Add("a a");

            List<string> justifiedLines = Justify(lines, maxLineWidth);
            PrintText(justifiedLines);


            bool isSuccess = false;

            return isSuccess;
        }

        private static bool Test5()
        {
            int maxLineWidth = 20;

            List<string> lines = new List<string>();
            lines.Add("a a");
            lines.Add("a a");

            List<string> justifiedLines = Justify(lines, maxLineWidth);
            PrintText(justifiedLines);


            bool isSuccess = false;

            return isSuccess;
        }
    }
}
