using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
    public class TokenRepository(IUsersRepository userRepository, IConfiguration configuration)
    {
        private readonly IUsersRepository iuserRepository = userRepository;
        readonly IConfiguration configuration = configuration;

        public string GenerateTokenJwt(string userEmail)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userEmail),
            };  

            var token = new JwtSecurityToken(
                   issuer: configuration.GetValue<string>("Jwt:Issuer"),
                   audience: configuration.GetValue<string>("Jwt:Audience"),
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key"));

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"), //.env
                    ValidateAudience = true,
                    ValidAudience = configuration.GetValue<string>("Jwt:Audience"), //.env
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (principal == null) throw new SecurityTokenException("Token inválido");

                var userIdClaim = principal.FindFirst(ClaimTypes.Email);

                if (userIdClaim == null) throw new SecurityTokenException("Não foi possível obter o userId do token");

                UsersModel user = await iuserRepository.FindUserByEmail(userIdClaim.Value);

                if (user == null) throw new SecurityTokenException("Usuário não encontrado");

                return user.Id;
            }
            catch (SecurityTokenException ex)
            {
                throw new SecurityTokenException("Token inválido", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao verificar o token", ex);
            }
        }
    }
}
