using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace EventLogger;

public class Function
{
    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
    {
        string eventType = input?.Headers?["x-event-type"] ?? "unknown";
        string body = input?.Body ?? "empty";

        context.Logger.LogLine($"Received Event Type: {eventType}");
        context.Logger.LogLine($"Payload: {body}");

        // TODO: Write to DynamoDB or S3

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = $"Event {eventType} logged."
        };
    }
}