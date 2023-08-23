namespace unittest
{
    public class CommandLineArgumentParserTests : IDisposable
    {
        CommandLineArgumentParser parser;

        public CommandLineArgumentParserTests() 
        {
            parser = new CommandLineArgumentParser();
        }

        public void Dispose()
        {
            //  close down test
            //  close connections to databases
        }


        [Fact]
        public void TestValidArgumentParsing()
        {
            string[] args = new string[] { "--input=file.txt", "--output=result.txt", "--verbose=true" };

            parser.ParseArguments(args);

            Assert.True(parser.HasArgument("input"));
            Assert.True(parser.HasArgument("output"));
            Assert.True(parser.HasArgument("verbose"));

            Assert.Equal("file.txt", parser.GetArgumentValue("input"));
            Assert.Equal("result.txt", parser.GetArgumentValue("output"));
            Assert.Equal("true", parser.GetArgumentValue("verbose"));
        }

        [Fact]
        public void TestInvalidArgumentParsing()
        {
            string[] args = new string[] { "--input=file.txt", "--invalid=option", "--output=result.txt" };

            parser.ParseArguments(args);

            Assert.True(parser.HasArgument("input"));
            Assert.False(parser.HasArgument("invalid"));
            Assert.True(parser.HasArgument("output"));
        }
    }
}