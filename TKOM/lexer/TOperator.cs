namespace TKOM.LexerN{
    public class TOperator : Token{

        public enum Operator{
            Assign, Equality, Inequal, Negation, Mult, Division, Eqless, Eqmore, Less, More, Minus, Plus, And, Or
        }

        public Operator Type { get; }
        public TOperator(char c1, char c2){
            if(c1 == '=') Type = Operator.Equality;
            else if(c1 == '!') Type = Operator.Inequal;
            else if(c1 == '<') Type = Operator.Eqless;
            else if(c1 == '>') Type = Operator.Eqmore;
        }
        public TOperator(char c)
        {
            switch(c)
            {
                case '=': 
                    Type = Operator.Assign; 
                    break;
                case '!': 
                    Type = Operator.Negation; 
                    break;
                case '*': 
                    Type = Operator.Mult; 
                    break;
                case '/': 
                    Type = Operator.Division; 
                    break;
                case '-': 
                    Type = Operator.Minus; 
                    break;
                case '+': 
                    Type = Operator.Plus; 
                    break;
                case '&': 
                    Type = Operator.And; 
                    break;
                case '|': 
                    Type = Operator.Or; 
                    break;
                case '>': 
                    Type = Operator.More; 
                    break;
                case '<': 
                    Type = Operator.Less; 
                    break;
            }
        }
        public static bool IsDoubleCharacterOperator(char c1, char c2)
        {
            if(c1 == '=' || c1 == '!' || c1 == '<' || c1 == '>')
                if(c2 == '=') return true;
            return false;
        }

        public override string Info()
        {
            return $"Type: OP\tValue: {Type.ToString()}";
        }
        
    }
}