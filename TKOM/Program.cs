using System;
using System.IO;
using TKOM.LexerN;

namespace TKOM
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = null;
            if(args.Length == 1)
            {
                if(File.Exists(args[0])==false)
                {
                    Console.WriteLine("File does not exist");
                    return;
                }
                code = new StreamReader(args[0]).ReadToEnd();
                Lex(code);
            }
            else{
                Console.Write("> ");
                while((code = Console.ReadLine()) != "exit")
                {
                    Lex(code);
                    Console.Write("> ");
                }
            }
        }

        static void Lex(string code)
        {
            Lexer lexer = new Lexer();
            var list = lexer.Lex(code);
            int i = 0;
            Console.WriteLine(lexer.Status.message);
            foreach(var element in list){
                Console.WriteLine($"ID: {(++i)}\t{element.token.Info()}\tPosition: [{element.line},{element.position}]");
            }
        }
    }
}