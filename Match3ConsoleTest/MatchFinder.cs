using System;
using System.Collections.Generic;

namespace Match3ConsoleTest {
    public class MatchFinder {

        public int MatchingThreshold = 3;
        public Board BoardInstance;

        // Handle board state input. Assuming colors == single characters. 1 row per line, 1 cell per character.
        // We're also assuming all lines are the same width, and no gaps.
        public void GetMatchesFromBoardState(string input) {
            var lineArray = input.Split("\r\n");
            var height = lineArray.Length;

            for (var index = 0; index < lineArray.Length; index++) {
                lineArray[index] = lineArray[index].Trim();
            }

            var width = lineArray[0].Length;

            BoardInstance = new Board(height, width);

            /*
             * We're assuming that we're receiving the input from top to bottom, left to right.
             * We're also assuming that all the given colors are valid, but we're throwing an exception if the character we get is not.
             * We'll build out array from the lower left corner.
             */
            for (var rowIndex = lineArray.Length - 1; rowIndex >= 0; rowIndex--) {
                var line = lineArray[rowIndex];
                for (var columnIndex = 0; columnIndex < line.Length; columnIndex++) {
                    BoardInstance.SetCellColor(line[columnIndex], new Tuple<int, int>(rowIndex, columnIndex));
                }
            }
            GetMatchesForAllCells(BoardInstance);
        }


        /*
         * Checking is done quasi-bruteforce since we have to check for horizontal and vertical matches.
         * As a result we're checking rows one column at a time and vise-versa.
         * However, we're also skipping the edges and checking only the cells that would have the required space on either side to have a match.
         * That means that for a 5x5 grid with a match threshold of 3, we're only checking 1 cell per row/column.
         * The end result of this is 10 checks (20 comparisons) for a 5x5 grid and 60 checks for a 10x10 grid, while using a matching threshold of 3.
         *
         */
        public void GetMatchesForAllCells(Board board) {
            
            var minimumJump = MatchingThreshold;
            var matchedList = new List<Cell>();
            var runNumber = 0;
            int offset;
            Console.WriteLine($"Matching a {board.Width}x{board.Height} board with a matching threshold of {MatchingThreshold}.\n");
            //Vertical checks
            for (var column = 0; column < board.Width; column++) {
                for (var row = minimumJump - 1; row < board.Height; row = row + minimumJump) {
                    //Check up
                    offset = 0;
                    while (MatchVertically(true, row + offset, column)) {
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row + offset, column)]);
                        ++offset;
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row + offset, column)]);
                    }
                    //Check down
                    offset = 0;
                    while (MatchVertically(false, row + offset, column)) {
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row + offset, column)]);
                        --offset;
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row + offset, column)]);
                    }
                    runNumber++;
                }
                //Check threshold
                if (matchedList.Count >= MatchingThreshold) {
                    foreach (var cell in matchedList) {
                        cell.MatchedVertically = true;
                    }
                    
                }

                matchedList.Clear();
            }
            
            //Horizontal checks
            for (int row = 0; row < board.Height; row++) {
                for (var column = minimumJump - 1; column < board.Width; column = column + minimumJump)
                {
                    //Check right
                    offset = 0;
                    while (MatchHorizontally(true, row, column + offset))
                    {
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row, column + offset)]);
                        ++offset;
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row, column + offset)]);
                    }
                    //Check left
                    offset = 0;
                    while (MatchHorizontally(false, row, column + offset))
                    {
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row, column + offset)]);


                        --offset;
                        matchedList.Add(board.CellArray[board.GetIndexFromInts(row, column + offset)]);

                    }
                    //Check threshold
                    if (matchedList.Count >= MatchingThreshold)
                    {
                        foreach (var cell in matchedList)
                        {
                            cell.MatchedHorizontally = true;

                        }
                    }
                    matchedList.Clear();
                }
            }

        }

        //Double-sided checks.
        private bool MatchVertically(bool up, int row, int column) {
            if (up) {
                if (row + 1 < BoardInstance.Height) {
                    return BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column)].CellColor ==
                           BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row + 1, column)].CellColor;
                }

                return false;

            }

            if (row - 1 >= 0) {
                return BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column)].CellColor ==
                       BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row - 1, column)].CellColor;
            }

            return false;


        }

        private bool MatchHorizontally(bool right, int row, int column) {
            if (right) {
                if (column + 1 < BoardInstance.Width) {
                    return BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column)].CellColor ==
                           BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column + 1)].CellColor;
                }

                return false;

            }

            if (column - 1 >= 0) {
                return BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column)].CellColor ==
                       BoardInstance.CellArray[BoardInstance.GetIndexFromInts(row, column - 1)].CellColor;
            }

            return false;


        }
       

    }
}
