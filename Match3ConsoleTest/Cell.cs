namespace Match3ConsoleTest
{
    public class Cell
    {
        public enum ColorEnum
        {
            Blue,
            Green,
            Red,
            Yellow
        }
        public ColorEnum CellColor { get; set; }
        public bool MatchedVertically { get; set; } 
        public bool MatchedHorizontally { get; set; }

    }
}