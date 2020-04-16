namespace TKOM.LexerN
{
    public class Token
    {
        public virtual string Info() {return "Invalid token";}
    }
    public class TUnknown : Token
    {
        public string Value {get;}
        public TUnknown(string value)
        {
            Value = value;
        }
    }
}