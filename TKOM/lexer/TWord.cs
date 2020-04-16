using System.Collections.Generic;

namespace TKOM.LexerN
{
    public class TWord : Token
    {
        public static List<string> Keywords = new List<string> {"Print", "Sort", "Filter", "Element", "Erase", "Sum", "Sublist", "PopFirst", "PopLast", "Count", "var", "list", "function", "while", "if", "return", "else"};
        public bool IsKeyword {get;}
        public string Value {get;}
        public TWord(string value)
        {
            if(Keywords.Contains(value))
                IsKeyword = true;
            Value = value;
        }

        public override string Info()
        {
            if(IsKeyword)
                return $"Type: KEY\tValue: {Value}";
            return $"Type: ID\tValue: {Value}";
        }
    }
}

