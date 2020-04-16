using System.IO;
using System.Globalization;
using Xunit;
using Xunit.Abstractions;
using TKOM.LexerN;

namespace TKOM.Tests
{
    public class LexerTests
    {
        private readonly ITestOutputHelper output;

        public LexerTests(ITestOutputHelper _output)
        {
            this.output = _output;
        }
        [Theory]
        [InlineData("identyfikator")]
        [InlineData("_id")]
        [InlineData("id123")]
        [InlineData("a1b2c3")]
        [InlineData("_ID")]        
        public void GivenSingleIDTokenItIsCorrectlyClassified(string id)
        {
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(id);
            Assert.False(lexer.Status.error);
            Assert.Single(tokens);
            Assert.True(tokens[0].token is TWord);
            Assert.True((tokens[0].token as TWord).IsKeyword == false);       
            Assert.True((tokens[0].token as TWord).Value == id);     
        }

        [Fact]
        public void GivenSingleOperatorTokenItIsCorrectlyClassified()
        {
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex("+");
            Assert.False(lexer.Status.error);
            Assert.Single(tokens);
            Assert.True(tokens[0].token is TOperator);
            Assert.True((tokens[0].token as TOperator).Type == TOperator.Operator.Plus);        
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.234")]
        [InlineData("987.1546")]
        public void GivenSingleNumberTokenItIsCorrectlyClassified(string number)
        {
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(number);
            Assert.False(lexer.Status.error);
            Assert.Single(tokens);
            Assert.True(tokens[0].token is TNumber);
            Assert.True((tokens[0].token as TNumber).Value == float.Parse(number, new CultureInfo("en-US")));        
        }

        [Fact]
        public void GivenSingleStringTokenItIsCorrectlyClassified()
        {
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex("\"aaaaaaaa\"");
            Assert.False(lexer.Status.error);
            Assert.Single(tokens);
            Assert.True(tokens[0].token is TString);
            Assert.True((tokens[0].token as TString).Value == "aaaaaaaa");        
        }

        [Fact]
        public void GivenProgramAllTokensAreCorrectlyClassified()
        {
            Lexer lexer = new Lexer();
            string code = new StreamReader("test").ReadToEnd();
            var tokens = lexer.Lex(code);
            Assert.Equal(90, tokens.Count);
        }

        [Fact]
        public void GivenProgramErrorHasRightPosition()
        {
            Lexer lexer = new Lexer();
            string code = new StreamReader("test_error").ReadToEnd();
            var tokens = lexer.Lex(code);
            Assert.True(lexer.Status.error);
            Assert.Equal(11, lexer.Status.line);
            Assert.Equal(17, lexer.Status.position);           
        }     

        [Theory]
        [InlineData("ó")]
        [InlineData(".")]
        [InlineData("Print($)")]
        [InlineData("\"łańcuch")]
        public void GivenInvalidCharacterStatusShowsError(string code)
        {
            Lexer lexer = new Lexer();
            lexer.Lex(code);
            Assert.True(lexer.Status.error);
        }
    }
}