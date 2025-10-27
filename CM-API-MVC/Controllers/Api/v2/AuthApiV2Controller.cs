using CM_API_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CM_API_MVC.Controllers.Api.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthApiV2Controller : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthApiV2Controller(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            {
                if (user.UserName == "admin" && user.Password == "password")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtKey = _config.GetValue<string>("JwtSettings:SecretKey");
                    if (string.IsNullOrWhiteSpace(jwtKey))
                        return BadRequest(new { Error = "JWT key is not configured." });
                    var key = Encoding.ASCII.GetBytes(jwtKey);


                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    try
                    {
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);
                        return Ok(new { Token = tokenString });
                    }
                    catch (Exception ex)
                    {
                        // Loga internamente (ou imprime pro console por enquanto)
                        Console.WriteLine($"Erro ao gerar token JWT: {ex.Message}");

                        // Retorna erro genérico pro cliente
                        return StatusCode(500, new { Error = "Erro interno ao gerar token." });
                    }

                }
                return Unauthorized();
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult TestLogin()
        {
            return Ok("Você está conectado!");
        }
    }
}
