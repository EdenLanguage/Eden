using Bogus;
using EdenClasslibrary.Errors;
using EdenClasslibrary.Types;

namespace EdenTests.ErrorTests
{
    public class ErrorTypesTests
    {
        private Faker<Token> _tokenGenerator;
        public ErrorTypesTests()
        {
            _tokenGenerator = new Faker<Token>()
                .RuleFor(x => x.Filename, f => f.System.FilePath())
                .RuleFor(x => x.LiteralValue, f => f.Random.String(length: 5, minChar:'a', maxChar:'z'))
                .RuleFor(x => x.Line, f => f.Random.Int(min: 0, max: 100))
                .RuleFor(x => x.TokenEndingLinePosition, f => f.Random.Int(min: 0, max: 100))
                .RuleFor(x => x.TokenStartingLinePosition, f => f.Random.Int(min: 0, max: 100));
        }

        [Fact]
        public void BasicTypes()
        {
            ErrorsManager errorManager = new ErrorsManager();
            errorManager.AppendError(ErrorDefaultParser.Create(ParserErrorType.InvalidSyntax, _tokenGenerator.Generate()));
            errorManager.AppendError(ErrorDefaultParser.Create(ParserErrorType.InvalidToken, _tokenGenerator.Generate()));

            AError error = errorManager.Errors.FirstOrDefault();
            string asStr = error.PrintError();
            Assert.NotNull(error);

            string allErrors = errorManager.PrintErrors();
        }
    }
}
