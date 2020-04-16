using System.Collections.Generic;
using System.Globalization;

namespace TKOM.LexerN
{
    public class Lexer{

        private static List<char> letters = new List<char> {'_', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        private static List<char> operatorChars = new List<char> {'=', '!', '*', '/', '-', '+', '<', '>', '|', '&'};
        private static List<char> separators = new List<char> {',', '(', ')', '{', '}', ';'};  

        private (bool error, int line, int position, string message) status  = (false, 0, 0, "No errors");
        public (bool error, int line, int position, string message) Status {get => status;}   

        public  List<(Token token, int line , int position)> Lex(string input)
        {
            int lineCounter = 1;
            int position = -1;
            List<(Token, int, int)> tokens = new List<(Token, int, int)>();
            for(int index = 0; index<input.Length && status.error == false;index++)
            {
                if(input[index] == '\n')
                {
                    lineCounter++;
                    position = index;
                }
                int startPosition = index - position;
                if(!char.IsWhiteSpace(input[index]))
                    if(letters.Contains(input[index]))
                        tokens.Add((GetWord(ref index, input),lineCounter,startPosition));
                    else if(char.IsDigit(input[index]))
                        tokens.Add((GetNumber(ref index, input),lineCounter,startPosition));
                    else if(operatorChars.Contains(input[index]))
                        tokens.Add((GetOperator(ref index, input),lineCounter,startPosition));
                    else if(separators.Contains(input[index]))
                        tokens.Add((GetSeparator(ref index, input),lineCounter,startPosition));
                    else if(input[index] == '"')
                        tokens.Add((GetString(ref index, input),lineCounter,startPosition));
                    else {
                        tokens.Add((new TUnknown("Unknown character"),lineCounter,startPosition));
                        status.message = "Tokenisation failed - unexpected character";
                        status.error = true;
                    }
                if(status.error == true) 
                {
                    status.line = lineCounter;
                    status.position = startPosition;
                    return tokens;
                }
            }
            return tokens;
        }

        private Token GetOperator(ref int i, string input){
            if(i+1 < input.Length && TOperator.IsDoubleCharacterOperator(input[i], input[i+1])){
                i++;
                return new TOperator(input[i-1], input[i]);
            }
            else 
                return new TOperator(input[i]);
        }

        private Token GetWord(ref int index, string input){
            int startIndex = index;
            for(;index < input.Length && (letters.Contains(input[index]) || char.IsDigit(input[index])); index++) ;
            return new TWord(input.Substring(startIndex, index-- - startIndex));
        }

        private Token GetNumber(ref int index, string input){
            int startIndex = index;
            for(;index < input.Length && char.IsDigit(input[index]); index++) ;
            if(index < input.Length && input[index] == '.'){
                index++;
                for(;index < input.Length && char.IsDigit(input[index]); index++) ;
            }
            return new TNumber(float.Parse(input.Substring(startIndex, index-- - startIndex),new CultureInfo("en-US")));
        }

        private Token GetSeparator(ref int index, string input){
            return new TSeparator(input[index]);
        }

        private Token GetString(ref int index, string input){
            int startIndex = index++;
            for(;index < input.Length && input[index] != '"'; index++) ;
            if(index == input.Length){
                status.message = "Tokenisation failed - string without end";
                status.error = true;
                return new Token();
            }
            return new TString(input.Substring(startIndex+1, index - startIndex - 1));
        }
    }
}