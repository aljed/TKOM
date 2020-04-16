using System.Collections.Generic;
using System;

namespace TKOM.LexerN
{
    public class Token
    {

        protected Token(){}
        public virtual string Info() {return "Unknown symbol";}
    }

    public class TOperator : Token{

        public enum Operator{
            Assignment, Equality, Inequality, Negation, Multiplication, Division, Eqless, Eqmore, Less, More, Minus, Plus, And, Or
        }

        public Operator Type { get; }
        public TOperator(char c1, char c2){
            if(c1 == '=') Type = Operator.Equality;
            else if(c1 == '!') Type = Operator.Inequality;
            else if(c1 == '<') Type = Operator.Eqless;
            else if(c1 == '>') Type = Operator.Eqmore;
        }
        public TOperator(char c)
        {
            switch(c)
            {
                case '=': 
                    Type = Operator.Assignment; 
                    break;
                case '!': 
                    Type = Operator.Negation; 
                    break;
                case '*': 
                    Type = Operator.Multiplication; 
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
            return Type.ToString();
        }
        
    }
    public class TWord : Token
    {
        public string Value {get;}
        public TWord(string value)
        {
            Value = value;
        }

        public override string Info()
        {
            return Value;
        }
    }
    public class TString : Token
    {
        public string Value {get;}
        public TString(string value)
        {
            Value = value;
        }
        public override string Info()
        {
            return Value;
        }
    }
    public class TNumber : Token
    {
        public float Value {get;}
        public TNumber(float value)
        {
            Value = value;
        }
        public override string Info()
        {
            return Value.ToString();
        }
    }
    public class TUnknown : Token
    {
        public string Value {get;}
        public TUnknown(string value)
        {
            Value = value;
        }
    }
    public class TSeparator : Token
    {

        public enum Separator{
            Comma, LeftB, RightB, LeftCB, RightCB, Semicolon
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
                    Type = Separator.Semicolon; 
                    break;
            }
        }

        public override string Info()
        {
            return Type.ToString();
        }
    }
}