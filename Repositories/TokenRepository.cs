using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Repositories
{
    public class TokenRepository(IUsersRepository iUserRepository, IConfiguration iConfiguration)
    {
        private readonly IUsersRepository iUserRepository = iUserRepository;
        private readonly IConfiguration iConfiguration = iConfiguration;

        public string GenerateTokenJwt(string userEmail)
        {
            var jwtKey = iConfiguration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("JWT Key cannot be null");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userEmail),
            };

            var token = new JwtSecurityToken(
                   issuer: iConfiguration.GetValue<string>("Jwt:Issuer"),
                   audience: iConfiguration.GetValue<string>("Jwt:Audience"),
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = iConfiguration.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("JWT Key cannot be null");
            var key = Encoding.UTF8.GetBytes(jwtKey);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = iConfiguration.GetValue<string>("Jwt:Issuer"),
                    ValidateAudience = true,
                    ValidAudience = iConfiguration.GetValue<string>("Jwt:Audience"),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (principal == null) throw new SecurityTokenException("Token inválido");

                var userIdClaim = principal.FindFirst(ClaimTypes.Email);

                if (userIdClaim == null) throw new SecurityTokenException("Não foi possível obter o userId do token");

                UsersModel user = await iUserRepository.FindUserByEmail(userIdClaim.Value);

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
