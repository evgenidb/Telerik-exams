using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greedy_Dwarf
{
    class Program
    {
        static void Main(string[] args)
        {
            string strValley = Console.ReadLine();


            char[] separators = { ',', ' ' };
            string[] strValleyArray = strValley.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int[] valley = new int[strValleyArray.Length];
            for (int index = 0; index < valley.Length; index++)
            {
                valley[index] = int.Parse(strValleyArray[index]);
            }

            int numOfPatterns = int.Parse(Console.ReadLine());
            int[][] patterns = new int[numOfPatterns][];

            for (int pattIndex = 0; pattIndex < numOfPatterns; pattIndex++)
            {
                string strPattern = Console.ReadLine();
                string[] strPatternArray = strPattern.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int[] pattern = new int[strPatternArray.Length];
                for (int index = 0; index < pattern.Length; index++)
                {
                    pattern[index] = int.Parse(strPatternArray[index]);
                }

                patterns[pattIndex] = pattern;
            }


            long maxGold = long.MinValue;
            long currentPatternGold = 0;
            List<int> currentPattenVisitedLinks = new List<int>();

            foreach (int[] pattern in patterns)
            {
                int currentPosition = 0;
                bool again = true;

                while (again)
                {
                    

                    foreach (int patternIndex in pattern)
                    {
                        // Calc the current position
                        
                        Console.WriteLine(currentPosition);

                        // Validate currentPosition
                        if (currentPosition < 0 || currentPosition >= valley.Length)
                        {
                            // Console.WriteLine("Break1 {0} {1}", currentPatternGold, currentPosition);
                            again = false;
                            break;
                        }

                        // Check if we were in this position before
                        foreach (int pos in currentPattenVisitedLinks)
                        {
                            if (pos == currentPosition)
                            {
                                // Console.WriteLine("Break2 {0} {1}", currentPatternGold, currentPosition);
                                again = false;
                                break;
                            }
                        }
                        if (!again)
                        {
                            break;
                        }
                        // Add position
                        currentPattenVisitedLinks.Add(currentPosition);


                        // Add the gold in this position
                        currentPatternGold += valley[currentPosition];

                        currentPosition += patternIndex;
                    }
                }

                if (currentPatternGold > maxGold)
                {
                    maxGold = currentPatternGold;
                }

                currentPattenVisitedLinks.Clear();
                
            }



            Console.WriteLine(maxGold);
        }
    }
}
