Simple ASP.NET Core API to test AWS:
* Elastic Beanstalk
* Lambda

## Elastic Beanstalk
Once I installed the `AWS Toolkit for Visual Studio` (Extensions -> Manage Extensions), right-clicking on project enabled the `Publish to AWS Elastic Beanstalk` option

It would be a good idea to create API Gateway (Type REST API) at this point. You'll want to add 2 sources:
* Method `ANY` on root that proxies to our Elastic Beanstalk URL
* Method `ANY` on path `{proxy+}` that proxies to our Elastic Beanstalk URL

Update - It turns out that you can create an API Gateway (Type HTTP) and achieve the same desired proxy for a fraction of the price. The UI in AWS Console makes it a little tricker to configure, but it's possible.

## Lambda
To publish to Lambda, I installed `Amazon.Lambda.Tools` globally:
```
dotnet tool install -g Amazon.Lambda.Tools
```

Then I added this to csproj file:

```
  <ItemGroup>
    <DotNetCliToolReference Include="Amazon.Lambda.Tools" Version="1.4.0" />
  </ItemGroup>
```

Added this file: LambdaEntryPoint.cs
```
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Startup>();
        }
    }
```

### Set up API Gateway...
...to expose the function as an HTTP REST API:

Right-click project, then Add -> AWS Serverless Template
Replase the following 2 lines:
* Handler
  * From `<ASSEMBLY-NAME>::<TYPE-FULL-NAME>::<METHOD-NAME>`
  * To `SimpleApi::SimpleApi.LambdaEntryPoint::FunctionHandlerAsync`
* Runtime
  * From `dotnetcore2.1`
  * To `dotnetcore3.1`

Right-click on project and select `Publish to AWS Lambda`

### Fix root proxy on API Gateway
After deployment, I was able to access `/products/1` and `products/2`, but root wasn't accessible. To fix this, I went to API Gateway in AWS Console and added an `ANY` method that proxied to our Lambda function.