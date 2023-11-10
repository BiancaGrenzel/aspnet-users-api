using System;
using System.Drawing.Drawing2D;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Autenticador.Domain.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Autenticador.API.Auth
{
    public class JwtIssuerOptions
    {
        public static string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(5);
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }

        public string UnixEpochDateIssuedAt
        {
            get
            {
                return ((long)Math.Round((IssuedAt.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds)).ToString();
            }
        }

        private bool Expired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityTokenHandler().ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            if (DateTime.UtcNow > jwt.ValidTo)
            {
                return true;
            }
            return false;
        }

        public AuthData GetAuthData(string token)
        {
            if (token == null || token == "Bearer null")
            {
                throw new Exception("Acesso não autorizado");
            }

            if (Expired(token))
            {
                throw new Exception("Autenticação expirada");
            }

            var jwt = new JwtSecurityTokenHandler().ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;

            AuthData authData = null;

            if (jwt.Claims.Count(claim => claim.Type == "sub") > 0)
            {
                var sub = jwt.Claims.First(claim => claim.Type == "sub").Value;
                authData = AuthData.Parse(sub);
            }
            else
            {
                throw new Exception("Token inválido");
            }

            return authData;
        }

        public async Task<string> Renew(string token)
        {
            if (Expired(token))
            {
                throw new Exception("Autenticação expirada");
            }
            var data = GetAuthData(token);
            return await Token(data);
        }

        public async Task<string> Token(AuthData authData)
        {

            var identity = new ClaimsIdentity(
                new GenericIdentity(authData.ToString(), "Token"),
                new[]
                {
                new Claim("Permissao", authData.Permissao.ToString())
                }
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, authData.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, await JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                        UnixEpochDateIssuedAt,
                        ClaimValueTypes.Integer64),
                        identity.FindFirst("Permissao")
            };


            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(ValidFor),
                signingCredentials: SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}