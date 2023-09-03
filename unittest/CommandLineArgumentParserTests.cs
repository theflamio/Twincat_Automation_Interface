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
            string[] args = new string[] { "--visualStudioFilePath=C:\\Dev\\TwinCatHelloWorld\\HelloWorld\\HelloWorld.sln", "--amsNetId=", "--compileSolution=false", "--runSolution=false", "--help=false" };

            parser.ParseArguments(args);

            Assert.True(parser.HasArgument("visualStudioFilePath"));
            Assert.True(parser.HasArgument("amsNetId"));
            Assert.True(parser.HasArgument("compileSolution"));
            Assert.True(parser.HasArgument("runSolution"));
            Assert.True(parser.HasArgument("help"));

            Assert.Equal("C:\\Dev\\TwinCatHelloWorld\\HelloWorld\\HelloWorld.sln", parser.GetArgumentValue("visualStudioFilePath"));
            Assert.Equal(string.Empty, parser.GetArgumentValue("amsNetId"));
            Assert.Equal("false", parser.GetArgumentValue("compileSolution"));
            Assert.Equal("false", parser.GetArgumentValue("runSolution"));
            Assert.Equal("false", parser.GetArgumentValue("help"));
        }

        [Fact]
        public void TestInvalidArgumentParsing()
        {
            string[] args = new string[] { "--visualStudioFilePath=C:\\Dev\\TwinCatHelloWorld\\HelloWorld\\HelloWorld.sln", "--amsNetId=", "--compileSolution=false", "--help=false" };

            parser.ParseArguments(args);

            Assert.True(parser.HasArgument("visualStudioFilePath"));
            Assert.True(parser.HasArgument("amsNetId"));
            Assert.True(parser.HasArgument("compileSolution"));
            Assert.False(parser.HasArgument("runSolution"));
            Assert.True(parser.HasArgument("help"));
        }
    }
}