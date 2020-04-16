namespace TKOM.LexerN
{    public class TNumber : Token
    {
        public float Value {get;}
        public TNumber(float value)
        {
            Value = value;
        }
        public override string Info()
        {
            return $"Type: NUMB\tValue: {Value.ToString()}";
        }
    }
}