namespace TKOM.LexerN
{
    public class TString : Token
    {
        public string Value {get;}
        public TString(string value)
        {
            Value = value;
        }
        public override string Info()
        {
            return $"Type: STRING\tValue: {Value}";
        }
    }
}