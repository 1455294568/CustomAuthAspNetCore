using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TestAuth2.Dbcontext;
using TestAuth2.Models;

namespace TestAuth2.Tools
{
    public class KevinAuth : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ApplicationDbContext _dbcontext;
        public KevinAuth(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory loggerFactory, UrlEncoder urlEncoder, ISystemClock systemClock, ApplicationDbContext dbContext)
            :base(options, loggerFactory, urlEncoder, systemClock)
        {
            _dbcontext = dbContext;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = Request.Cookies["Token"];
                if (token == null)
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }
                var result = AesTool.Decrypt(token);
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(result);
                var claims = new[] { new Claim(ClaimTypes.Name, info.username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                var u = _dbcontext.Users.Find(info.Id);
                if (u != null)
                {
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return Task.FromResult(AuthenticateResult.Fail("No a valid user"));
                }
            }
            catch(Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail("error"));
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            
            Response.Redirect("/home/login");
            return Task.CompletedTask;
        }
    }

    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
}
