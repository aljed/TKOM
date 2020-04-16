using System.Collections.Generic;

namespace TKOM.LexerN
{
    class Lexer{

        private static List<char> letters = new List<char> {'_', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        private static List<char> digits = new List<char> {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private static List<char> operatorChars = new List<char> {'=', '!', '*', '/', '-', '+', '<', '>', '|', '&'};
        private static List<char> separators = new List<char> {',', '(', ')', '{', '}', ';'};        
        // private delegate Token GetTok(ref int index, string input);
        // private  Dictionary<char, GetTok> CharacterMap = new Dictionary<char, GetTok>();

        // public Lexer(){
        //     foreach(char c in letters){
        //         CharacterMap.Add(c, GetWord);
        //     }
        //     foreach(char c in digits){
        //         CharacterMap.Add(c, GetNumber);
        //     }
        //     foreach(char c in operatorChars){
        //         CharacterMap.Add(c, GetOperator);
        //     }
        //     foreach(char c in separators){
        //         CharacterMap.Add(c, GetSeparator);
        //     }
        //     CharacterMap.Add('"', GetString);
        // }
        public  List<Token> Lex(string input)
        {
            List<Token> tokens = new List<Token>();
            for(int index = 0;index<input.Length;index++)
            {
                if(!char.IsWhiteSpace(input[index]))
                    if(letters.Contains(input[index]))
                        GetWord(ref index, input);
                    else if(digits.Contains(input[index]))
                        GetNumber(ref index, input);
                    else if(operatorChars.Contains(input[index]))
                        GetOperator(ref index, input);
                    else if(separators.Contains(input[index]))
                        GetSeparator(ref index, input);
                    // try{
                    //     tokens.Add(CharacterMap[input[index]](ref index, input));
                    // }
                    // catch (KeyNotFoundException){
                    //     tokens.Add(new TUnknown(input[index].ToString()));
                    // }
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
            for(;index < input.Length && (letters.Contains(input[index]) || digits.Contains(input[index])); index++) ;
            return new TWord(input.Substring(startIndex, index-- - startIndex));
        }

        private Token GetNumber(ref int index, string input){
            int startIndex = index;
            for(;index < input.Length && digits.Contains(input[index]); index++) ;
            if(index < input.Length && input[index] == '.'){
                index++;
                for(;index < input.Length && digits.Contains(input[index]); index++) ;
            }
            return new TNumber(float.Parse(input.Substring(startIndex, index-- - startIndex)));
        }

        private Token GetSeparator(ref int index, string input){
            return new TSeparator(input[index]);
        }

        private Token GetString(ref int index, string input){
            int startIndex = index++;
            for(;input[index] != '"'; index++) ;
            return new TString(input.Substring(startIndex+1, index - startIndex - 1));
        }

    }
}