namespace Typing
{
    public class AWord
    {
        public string word {get; set;}
        public Size area {
            get { return TextRenderer.MeasureText(word, font); }
        }
        public float pos_x {get; set;}
        public float pos_y {get; set;}

        public int matchNum {get; set;}

        public Font font {get; set;}

        public AWord(string s, Font f, float x, float y)
        {
            word = s;
            font = f;
            pos_x = x;
            pos_y = y;
        }
    }

    public class Globals
    {
        public static string[] Family = {
            "friend", "father", "mother", "brother", "sister", "cousin", "pet", "grandfather", "grandmother"
        };
        public static string[] SomeWords = {
            "weed", "ham", "bicycle", "brotherhood", "microphone", "circus", "peanut", "windmil", "property",
            "tree", "math", "question", "system", "computer", "brush", "nubra", "chair", "telephone", "drug",
            "paper", "machine", "backpack", "cup", "bottle", "speaker", "pipe", "lighter", "eraser", "robot",
            "algorithm", "fan", "darkness", "triangle", "zoo", "hotel", "compilation", "keyboard", "jaggler",
            "wheel", "ninja", "ghost", "yard", "unicorn", "virus", "meditation", "clock", "fairy", "balloon",
            "square", "escape", "knight", "youth", "gentleman", "envy", "equality", "raincoat", "attraction",
            "fortune", "country", "guide", "helicopter", "struggle", "trip", "dish", "adjustment", "weather",
            "frequency", "license", "freedom", "circle", "earth", "wolrd", "crime", "stress", "investigator",
        };
        public static string[] ThreeWords = {
            "fit", "toe", "run", "mob", "ear", "flu", "tea", "gap", "key", "tic", "pot", "tag", "fee", "map",
            "pad", "rip", "sty", "man", "bag", "son", "ash", "doe", "tan", "rag", "obi", "bit", "die", "wit",
            "hat", "rye", "rim", "ego", "eel", "age", "cat", "dog", "rat", "set", "sir", "pay", "bat", "pic",
        };
    }
}