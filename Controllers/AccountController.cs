using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NaijaPidginAPI.DTOs.UserDTO;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Extentions;
using NaijaPidginAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }
        /// <summary>
        /// Login for registered users 
        /// </summary>>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await unitOfWork.UserRepository.Authenticate(loginRequestDTO.Username, loginRequestDTO.Password);
            var disabledUser = await unitOfWork.UserRepository.UserDisabled(loginRequestDTO.Username);

            if(user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            if(disabledUser == true)
            {
                return Unauthorized("You have been disabled from the system for going against the community policy");
            }

            var loginResponse = new LoginResponseDTO();
            loginResponse.Username = user.Username;
            loginResponse.Token = CreateJWT(user);

            return Ok(loginResponse);

        }


        /// <summary>
        /// Registers new users 
        /// </summary>>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(registerDTO.Username.IsEmpty() || registerDTO.Password.IsEmpty())
            {
                return BadRequest("Username or password cannot be empty");
            }

            if (await unitOfWork.UserRepository.UserAlreadyExists(registerDTO.Username))
                return BadRequest("Username already exists, please try another username");

            unitOfWork.UserRepository.Register(registerDTO);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }


        //[Authorize]
        //[HttpPost("change-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO passwordDTO)
        //{
        //    var loggedInUser = GetUserId();

        //    var user = await unitOfWork.UserRepository.GetUserByIdAsync(loggedInUser);

        //    var userName = await userManager.FindByNameAsync(user.Username);
           
        //    if (user == null)
        //        return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "User does not exist." });

        //    if (string.Compare(passwordDTO.NewPassword, passwordDTO.ConfirmPassword) != 0)
        //        return StatusCode(StatusCodes.Status400BadRequest, new { status = false, message = "The new password and confirm new password does not match" });
            
        //    var result = await userManager.ChangePasswordAsync(userName, passwordDTO.OldPassword, passwordDTO.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        var errors = new List<string>();
        //        foreach (var error in result.Errors)
        //        {
        //            errors.Add(error.Description);
        //        }
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { status = false, message = string.Join(", ", errors) });
        //    }
        //    return Ok(new { status = true, message = "Password changed successfully" });
        //}


        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
