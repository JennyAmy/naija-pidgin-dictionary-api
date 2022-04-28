using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaijaPidginAPI.DTOs.UserDTO;
using NaijaPidginAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await unitOfWork.UserRepository.GetUsersAsync();
            var userDTO = mapper.Map<IEnumerable<ListUsersDTO>>(users);
            return Ok(userDTO);
        }


        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            var userDTO = mapper.Map<ListUsersDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet("list-admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await unitOfWork.UserRepository.GetAdminsAsync();
            var userDTO = mapper.Map<IEnumerable<ListUsersDTO>>(admins);
            return Ok(userDTO);
        }

        [HttpGet("list-nonadmins")]
        public async Task<IActionResult> GetAllNonAdmins()
        {
            var nonAdmins = await unitOfWork.UserRepository.GetNonAdminsAsync();
            var userDTO = mapper.Map<IEnumerable<ListUsersDTO>>(nonAdmins);
            return Ok(userDTO);
        }

        //Manually make or remove an admin
        [HttpPut("make-admin/{userId}")]
        [Authorize]
        public async Task<IActionResult> MakeOrRemoveAdmin(int userId)
        {
            var loggedInUser = GetUserId();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (await unitOfWork.UserRepository.UserIsAdmin(loggedInUser))
            {
                if (!user.IsAdmin)
                {
                    user.IsAdmin = true;
                }
                else
                {
                    user.IsAdmin = false;
                }
                   
            }
            else
            {
                return Unauthorized("You are not authorised to carry out this action");
            }

            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        // Automatically makes a user an admin
        [HttpPut("automatic-admin")]
        [Authorize]
        public async Task<IActionResult> AutomaticAdmin()
        {
            var loggedInUserId = GetUserId();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(loggedInUserId);
            int totalWords = unitOfWork.WordRepository.CountApprovedWordsByUserId(loggedInUserId);

            //Automatically makes a user an admin if the words added 
            if (await unitOfWork.UserRepository.UserIsAdmin(loggedInUserId) == false)
            {
                if (totalWords >= 10)
                {   
                    user.IsAdmin = true;
                }
            }

            if (await unitOfWork.SaveAsync()) return StatusCode(200);
            return StatusCode(200);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(UpdateDTO updateDTO)
        {
            var userId = GetUserId();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if (await unitOfWork.UserRepository.UserAlreadyExists(updateDTO.Username, user.UserId))
                return BadRequest("Username already exists, please try another username");

            else
            {
                user.LastUpdatedBy = userId;
                user.LastUpdatedOn = DateTime.Now;
                mapper.Map(updateDTO, user);
                await unitOfWork.SaveAsync();
            }

            return StatusCode(200);

        }

        [HttpPut("disable-user/{userId}")]
        [Authorize]
        public async Task<IActionResult> DisableUser(int userId)
        {
            var loggedInUser = GetUserId();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);

            if(await unitOfWork.UserRepository.UserIsAdmin(loggedInUser))
            {
                if (!user.IsDisabled)
                {
                    user.IsDisabled = true;
                    user.DisabledBy = loggedInUser;
                }
                else
                {
                    user.IsDisabled = false;
                }   
            }
            else
            {
                return Unauthorized("You are not authorised to carry out this action");
            }
            await unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        [HttpGet]
        [Route("isadmin/{userId}")]
        public async Task<IActionResult> isAdmin(int userId)
        {
            var result = await unitOfWork.UserRepository.UserIsAdmin(userId);

            return result == false ? Unauthorized() : StatusCode(200);
        }

        [AllowAnonymous]
        [HttpGet("users-count")]
        public IActionResult TotalUsersCount()
        {
            int totalUsers =  unitOfWork.UserRepository.CountTotalUsers();

            return Ok(new
            {
                status = true,
                data = totalUsers
            });
        }


        [AllowAnonymous]
        [HttpPost("user-exists")]
        public async Task<IActionResult> UserExists(RegisterDTO registerDTO)
        {
            var result = await unitOfWork.UserRepository.UserAlreadyExists(registerDTO.Username);

            return result == true ? BadRequest("Username already exists, please try another username") : StatusCode(200);

        }

    }
}
