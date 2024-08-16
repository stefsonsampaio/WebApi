using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Infra
{
    public class TokenExampleRequest : IExamplesProvider<TokenResponse>
    {
        public TokenResponse GetExamples()
        {
            return new TokenResponse
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwiaWF0IjoxNjM3NzgxNjA5LCJleHBpcmF0aW9uIjoiY2hhcmFjdGVyQG5vbmUifQ.nOV0IR6m5gF_YVgEYnh0Go4OZPaL5H6KKZg5y3Jvhlc"
            };
        }
    }
}
