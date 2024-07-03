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
    public class TokenRepository(IUsersRepository userRepository)
    {
        private readonly IUsersRepository iuserRepository = userRepository;
        readonly string SecretKey = "bad07acd-d70b-478d-99b1-8fbe825824dc"; //.env

        public string GenerateTokenJwt(string userEmail)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userEmail),
            };  

            var token = new JwtSecurityToken(
                   issuer: "sua_empresa", //.env
                   audience: "sua_aplicacao", //.env
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "sua_empresa", //.env
                    ValidateAudience = true,
                    ValidAudience = "sua_aplicacao", //.env
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
