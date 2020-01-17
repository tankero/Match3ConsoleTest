using System;
using System.Text;

namespace Match3ConsoleTest {
    //Simple class to represent cells.
    public class Cell {

        public char CellColor { get; set; }
        public bool MatchedVertically { get; set; }
        public bool MatchedHorizontally { get; set; }

    }

    public class Board
    {
        // We could have a more complex structure of 2D arrays, or a dictionary of arrays, etc.. but storing all cells in a single, flat structure is generally cleaner.
        public Cell[] CellArray;

        public int Width { get; set; }
        public int Height { get; set; }

        public Board(int height, int width)
        {
            Height = height;
            Width = width;
            CellArray = new Cell[Height * Width];
        }


        public void SetCellColor(char colorChar, Tuple<int, int> coordinates)
        {

            var (row, column) = coordinates;
            CellArray[GetIndexFromInts(row, column)] = new Cell
            {
                CellColor = colorChar
            };
        }


        // Helper function simplifies looking up the proper index. Supports row-major and column-major grids.
        public int GetIndexFromInts(int row, int column) {
            return Width >= Height? row * Width + column : column * Height + row;
        }


        //If the cell is part of a match, display the character. Otherwise, display an asterisk.
        public string BoardState()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    builder.Append(
                        CellArray[GetIndexFromInts(i, j)].MatchedHorizontally ||
                        CellArray[GetIndexFromInts(i, j)].MatchedVertically
                            ? CellArray[GetIndexFromInts(i, j)].CellColor
                            : '*');
                }

                builder.Append('\n');
            }

            return builder.ToString();
        }
        
    }
}