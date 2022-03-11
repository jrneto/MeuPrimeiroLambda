using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MaisControleApiGateway;

public class Function
{

    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        LambdaLogger.Log("In�cio execu��o\n");

        string id = String.Empty;

        if (request != null)
        {
            id = request.QueryStringParameters?.FirstOrDefault(x => x.Key == "id").Value ?? "0";

            if (!int.TryParse(id, out _))
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "O par�metro id s� aceita valores num�ricos. Id informado: " + id
                };
            }
        }

        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = "Ol�! A hora atual �: " + DateTime.Now.ToString("hh:mm:ss") + "\n" +
                   "FunctionName: " + context.FunctionName + "\n" +
                   "AwsRequestId: " + context.AwsRequestId + "\n" +
                   "FunctionVersion: " + context.FunctionVersion + "\n" +
                   "id: " + id,
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        LambdaLogger.Log("Fim execu��o\n");

        return response;
    }
}
