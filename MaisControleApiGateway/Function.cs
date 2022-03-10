using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MaisControleApiGateway;

public class Function
{

    public APIGatewayProxyResponse FunctionHandler(ILambdaContext context)
    {
        LambdaLogger.Log("Início execução\n");

        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = "Olá! A hora atual é: " + DateTime.Now.ToString("hh:mm:ss") + "\n" +
                   "FunctionName: " + context.FunctionName + "\n" +
                   "AwsRequestId: " + context.AwsRequestId + "\n" +
                   "FunctionVersion: " + context.FunctionVersion,
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        LambdaLogger.Log("Fim execução\n");

        return response;
    }
}
