
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Fincore.Models;
using Fincore.Models.DTOs;
using Fincore.DBclasses;

namespace Fincore.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthDBclss dbobj = new AuthDBclss();

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("customerregister")]
        public IActionResult CustomerRegister(CustomerRegisterDTO obj)
        {
            Customerclss cobj = new Customerclss();

            cobj.customer_name = obj.customer_name;
            cobj.age = obj.age;
            cobj.email = obj.email;
            cobj.phone = obj.phone;
            cobj.address = obj.address;
            cobj.username = obj.username;
            cobj.password = obj.password;

            int id = dbobj.CustomerRegisterDB(cobj);

            return Ok(new { message = "Customer registered successfully", customer_id = id });
        }

        [HttpPost("adminregister")]
        public IActionResult AdminRegister(AdminRegisterDTO obj)
        {
            Adminclss aobj = new Adminclss();

            aobj.admin_name = obj.admin_name;
            aobj.admin_email = obj.admin_email;
            aobj.username = obj.username;
            aobj.password = obj.password;

            int id = dbobj.AdminRegisterDB(aobj);

            return Ok(new { message = "Admin registered successfully", admin_id = id });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO obj)
        {
            Loginclss lobj = new Loginclss();

            lobj.username = obj.username;
            lobj.password = obj.password;

            var user = dbobj.LoginDB(lobj);

            if (user.reg_id != 0)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.login_id.ToString()),
                    new Claim("reg_id", user.reg_id.ToString()),
                    new Claim(ClaimTypes.Role, user.role!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = CreateToken(authClaims);

                TokenResponseDTO response = new TokenResponseDTO();

                response.token = new JwtSecurityTokenHandler().WriteToken(token);
                response.login_id = user.login_id;
                response.reg_id = user.reg_id;
                response.role = user.role;

                return Ok(response);
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)
            );

            int.TryParse(
                _configuration["JWT:TokenValidityInMinutes"],
                out int tokenValidityInMinutes
            );

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return token;
        }
    }
}
