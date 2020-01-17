using System;
using System.Text;

namespace Match3ConsoleTest {
    internal class Program {

        /*
         * Unit tests for this would include the input validation we're doing below, making sure that the navigation is landing at the right spot, matching is working properly, etc..
         * Didn't include them this time around due to time constraints on my side, just wanted to make sure to let you know that they were a consideration.
         * The board supports square and rectangular dimensions.
         *
         */

        private static void Main(string[] args) {
            Console.WriteLine("Match 3 test! \nCopy paste the board state you want to use and hit enter.\nInput can be entered as a multi-line block, or line by line.\n**All lines have to have the same width.**\nTo match, enter an empty string.");
            StringBuilder builder = new StringBuilder();
            int lineLength = 0;
            while (true) {
                var input = Console.ReadLine();

                if (input == string.Empty) {
                    break;
                }

                if (builder.Length == 0 && input != null) {
                    lineLength = input.Length;
                }

                //Making sure we don't have any gaps.
                if (input != null && input.TrimEnd().Length != lineLength) {
                    Console.WriteLine();
                    Console.WriteLine("The line length wasn't constant. Hit enter to quit.");
                    Console.ReadLine();
                    return;
                }

                builder.AppendLine(input);
            }
            var matcher = new MatchFinder();

            //The only exception we're throwing right now is an invalid color input.
            try
            {
                matcher.GetMatchesFromBoardState(builder.ToString().TrimEnd());
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while trying to evaluate the submitted board state: {e.Message}. Quitting.");
                Console.ReadLine();
                return;
            }

            Console.Write(matcher.BoardInstance.BoardState());
            Console.WriteLine("\nMatching complete! Hit any key to quit.");
            Console.ReadLine();
        }
    }
}
