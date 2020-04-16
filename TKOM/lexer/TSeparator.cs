namespace TKOM.LexerN{
public class TSeparator : Token
    {

        public enum Separator{
            Comma, LeftB, RightB, LeftCB, RightCB, Semicol
        }
        public Separator Type {get;}
        public TSeparator(char c)
        {
            switch(c)
            {
                case ',': 
                    Type = Separator.Comma; 
                    break;
                case '}': 
                    Type = Separator.RightCB; 
                    break;
                case '{': 
                    Type = Separator.LeftCB; 
                    break;
                case '(': 
                    Type = Separator.LeftB; 
                    break;
                case ')': 
                    Type = Separator.RightB; 
                    break;
                case ';': 
                    Type = Separator.Semicol; 
                    break;
            }
        }

        public override string Info()
        {
            return $"Type: SEP\tValue: {Type.ToString()}";
        }
    }
}