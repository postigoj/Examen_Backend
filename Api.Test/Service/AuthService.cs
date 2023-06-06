using Api.Test.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Test.Service
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        public IConfiguration _configuration;

        public AuthService(UserRepository userRepository, IConfiguration config)
        {
            _configuration = config;
            _userRepository = userRepository;
        }


        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetUser(username, password);

            if (user != null)
            { 
                //genera el token jwt bearer
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Usuario", user.Usuario)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                
                return new JwtSecurityTokenHandler().WriteToken(token);
                
            }
              
            return null;
            }
        }
}


