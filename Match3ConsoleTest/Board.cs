using System;
using System.Linq;
using System.Text;

namespace Match3ConsoleTest {
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


        public char[] ColorChars = {
            'B', 'G', 'R', 'Y'
        };

        public void SetCellColor(char colorChar, Tuple<int, int> coordinates)
        {
            if (!ColorChars.Contains(colorChar))
            {
                throw new ArgumentException($"The color character is not valid: {colorChar}\n\nValid range is: {ColorChars}");
            }

            var (row, column) = coordinates;
            CellArray[GetIndexFromInts(row, column)] = new Cell
            {
                CellColor = (Cell.ColorEnum) Array.IndexOf(ColorChars, colorChar)
            };
        }


        // Helper function simplifies looking up the proper index. Supports row-major and column-major grids.
        public int GetIndexFromInts(int row, int column) {
            return Width >= Height? row * Width + column : column * Height + row;
        }

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
                            ? ColorChars[(int) CellArray[GetIndexFromInts(i, j)].CellColor]
                            : '*');
                }

                builder.Append('\n');
            }

            return builder.ToString();
        }
        
    }
}