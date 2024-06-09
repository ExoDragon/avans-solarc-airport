using AirportManagementApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagementApi.Controllers
{
    [Route("api/v1/security")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SecurityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public SecurityController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model"></param>  
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    IList<Claim> claims = await _userManager.GetClaimsAsync(user);

                    JwtSecurityToken token = CreateToken(user.UserName, claims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return Unauthorized();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="model"></param>  
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser userExists = await _userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                {
                    return BadRequest();
                }

                IdentityUser user = new IdentityUser(model.Email) { Email = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                Claim claim = new Claim(model.Claim, "true");
                IList<Claim> claims = new List<Claim>() 
                {
                    claim
                };

                await _userManager.AddClaimAsync(user, new Claim(model.Claim, "true"));

                JwtSecurityToken token = CreateToken(user.UserName, claims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest();
        }

        private JwtSecurityToken CreateToken(string username, IList<Claim> claims)
        {
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
