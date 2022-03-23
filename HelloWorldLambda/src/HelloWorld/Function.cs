using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld
{

    public class Function
    {

        private static readonly HttpClient client = new HttpClient();

        private static async Task<string> GetCallingIP()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

            var msg = await client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext: false);

            return msg.Replace("\n", "");
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {


            if (apigProxyEvent == null)
            {
                var location = await GetCallingIP();
                var body = new Dictionary<string, string>
                {
                    { "message", "hello world" },
                    { "location", location }
                };

                return new APIGatewayProxyResponse
                {
                    Body = JsonSerializer.Serialize(body),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

            }

            var httpMethod = apigProxyEvent?.HttpMethod?.ToUpper();

            Console.WriteLine($"############## Method: {httpMethod}");

            if (httpMethod == "GET")
            {                
                return new APIGatewayProxyResponse
                {
                    Body = JsonSerializer.Serialize($"Não tem body! É um {httpMethod}"),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Http-Method", httpMethod }
                    }
                };
            }


            if (httpMethod == "POST")
            {
                return new APIGatewayProxyResponse
                {
                    Body = JsonSerializer.Serialize(apigProxyEvent.Body ?? $"request sem body. {httpMethod}"),
                    StatusCode = 200,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Http-Method", httpMethod }
                    }
                };
            }

            return new APIGatewayProxyResponse
            {
                Body = JsonSerializer.Serialize($"É um {httpMethod}"),
                StatusCode = 200,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Http-Method", httpMethod }
                }
            };
        }
    }
}
