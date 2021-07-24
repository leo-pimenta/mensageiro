using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace App.Auth
{
    public class TokenValidationEvents
    {
        public Task OnMessageReceived(MessageReceivedContext context)
        {
            string accessTokenQuery = context.Request.Query["access_token"];
            string accessTokenHeader = context.Request.Headers["Authorization"];
            string accessToken = accessTokenHeader ?? accessTokenQuery;
            PathString path = context.HttpContext.Request.Path;

            if (path.StartsWithSegments("/chat") && !string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken.Replace("Bearer ", "");
            }

            return Task.CompletedTask;
        }
    }
}