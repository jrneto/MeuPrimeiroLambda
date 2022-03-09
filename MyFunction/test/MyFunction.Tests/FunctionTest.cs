using Amazon.Lambda.TestUtilities;
using Xunit;

namespace MyFunction.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var upperCase = function.FunctionHandler(new InputType { Nome = "Neto" }, context);

        Assert.Equal("Olá NETO!", upperCase);
    }

    [Fact]
    public void ShouldAvoidNullReferenceException()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var upperCase = function.FunctionHandler(null, context);

        Assert.Equal("Olá !", upperCase);
    }
}
