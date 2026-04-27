using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIZZAORDERING.Data;
using PIZZAORDERING.Dto;
using PIZZAORDERING.Model;
using PIZZAORDERING.Models;
using PIZZAORDERING.Services;

namespace PIZZAORDERING.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _tokenServices;
        private readonly AppDbContext _Db;
        public AuthController(AuthServices token, AppDbContext App)
        {
            _tokenServices = token;
            _Db = App;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var userifexsist =await _Db.Users.AnyAsync(u => u.Email == request.Email);

            if (userifexsist)
            {
                return BadRequest(new { message = "user already exsist" });
            }
            
            var passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordhash,
                Address = request.Address,
                LoyaltyPoints =0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                Role = "User"
            };

            await _Db.Users.AddAsync(user);
            await _Db.SaveChangesAsync();
            return Ok(new { message = "registered" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _Db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Wrong Credentials" });
            }

            var passwordiscorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordiscorrect)
            {
                return BadRequest(new { message = "enter the right credentials" });
            }

            var generatetoken = _tokenServices.GenerateToken(user.FullName,request.Email, user.Role);

            var refreshtoken = _tokenServices.GenerateRefresh();

            var refreshexpriy = _tokenServices.GetRefreshTokenExpiry();

            user.RefreshToken = refreshtoken;
            user.RefreshTokenExpiryTime = refreshexpriy;
            await _Db.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                Token = generatetoken,
                RefreshToken = refreshtoken
            });
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
            var userdetail = await _Db.Users.FirstOrDefaultAsync(u =>
                u.RefreshToken == request.RefreshToken);

            if (userdetail == null)
            {
                return Unauthorized(new { message = "sorry invalid token" });
            }

            if (userdetail.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "token time expired" });
            }

            var newtoken = _tokenServices.GenerateToken(userdetail.FullName,userdetail.Email, userdetail.Role);

            var refresh = _tokenServices.GenerateRefresh();
            var refreshtime = _tokenServices.GetRefreshTokenExpiry();

            userdetail.RefreshToken = refresh;
            userdetail.RefreshTokenExpiryTime = refreshtime;
            await _Db.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                Token = newtoken,
                RefreshToken = refresh
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> logout([FromBody] RefreshRequestDto request)
        {
            var userdetail = await _Db.Users.FirstOrDefaultAsync(u =>
        
                u.RefreshToken == request.RefreshToken
            );

            if (userdetail == null)
            {
                return Unauthorized(new { message = "wrong token" });
            }

            userdetail.RefreshToken = null;
            userdetail.RefreshTokenExpiryTime = null;

            await _Db.SaveChangesAsync();

            return Ok(new { message = "logout success" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getusers")]
        public async Task<IActionResult> getuser()
        {
            var list = await _Db.Users.ToListAsync();
            var result = new List<UpdateProfileDto>();
            foreach (var n in list)
            {
                var now = new UpdateProfileDto
                {
                    Fullname = n.FullName,
                    PhoneNumber = n.PhoneNumber,
                    Address = n.Address
                };
                result.Add(now);
            }

            return Ok(result);
        }
    }