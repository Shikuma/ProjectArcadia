public class GamePrefs {

    //Add player prefs, like high scores, to be used in the specified game mode
    public static string GameModeName { get; set; }

    public struct SudokuPrefs {
        public enum Difficulties { EASY, MEDIUM, HARD }
        public enum BoardSizes { SMALL, NORMAL, LARGE }
        public static Difficulties difficulty { get; set; }
        public static BoardSizes boardSize { get; set; }
        
    }

    public struct TTCPrefs {
        public static int SomeProperty { get; set; }
    }

}