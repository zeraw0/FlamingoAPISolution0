using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this line
using Flamingo_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Flamingo_API.Helpers;


using ConfigurationManager = Flamingo_API.Helpers.ConfigurationManager;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Flamingo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repo.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/User/email/example@example.com
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _repo.GetByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _repo.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }



        // POST api/<AuthenticationController>
        [HttpPost("login")]
        public IActionResult Post([FromBody] Login user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            if (_repo.ValidateUser(user.Email, user.Password, user.Role))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["Jwt:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["Jwt:ValidAudience"],
                    //claims: new List<Claim>(),
                    claims: new List<Claim>(new Claim[] {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role,user.Role)
                                   }),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                ); ;
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse { Token = tokenString });
            }
            return Unauthorized();
        }







        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (user == null || id <= 0)
            {
                return BadRequest("Invalid user data.");
            }

            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.PhoneNo = user.PhoneNo;
            existingUser.Role = user.Role;

            try
            {
                await _repo.UpdateAsync(existingUser);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details and return a server error
                Console.WriteLine($"Error updating user: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the user.");
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }



    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string Role { get; set; }

    }

}
