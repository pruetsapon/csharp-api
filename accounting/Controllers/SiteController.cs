using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Accounting.WS.Models.Config;
using Accounting.WS.Models.DB;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.WS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SiteController : Controller
    {
        private readonly WebConfig _webConfig;
        private readonly SystemDbContext _context;
        public SiteController (
            IOptions<WebConfig> webConfig,
            SystemDbContext context)
        {
            _context = context;
            _webConfig = webConfig.Value;
        }

        // POST api/site/login
        [HttpPost("login")]
        [AllowAnonymous]
        public object SiteLogin([FromBody] string tokenKey)
        {
            //find site in db
            var site = _context.Site.SingleOrDefault( s => s.TokenKey == tokenKey);
            if( site != null )
            {
                return GenerateJwtToken(site);
            }
            return BadRequest();
        }

        private object GenerateJwtToken(Site site)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, site.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, site.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_webConfig.Authentication.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_webConfig.Authentication.ExpiredDate));

            var token = new JwtSecurityToken(
                _webConfig.Authentication.Domain,
                _webConfig.Authentication.Domain,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
