using System;
using TKOM.LexerN;

namespace TKOM
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer();
            var list = lexer.Lex(Console.ReadLine());
            foreach(var token in list){
                Console.WriteLine(token.Info());
            }
        }
    }
}