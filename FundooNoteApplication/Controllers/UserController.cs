using BusinessLayer.Interface;
using BusinessLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                logger.LogInformation("Registration started");
                var result = userBL.UserRegistrations(userRegistration);
                if (result != null)
                {
                    logger.LogInformation("Registration is successful");
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "User Registration successfull", Data = result });
                }
                else
                {
                    logger.LogError("Registration failed..");
                    return this.BadRequest(new { success = false, message = "User Registration unsuccessufull" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
               
                var result = userBL.UserLogins(userLogin);

                if (result != null)
                {
                    HttpContext.Session.SetInt32("UserID", result.UserId);
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "Login successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Invalid login credentials" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("check-email-exists")]
        public IActionResult checkEmailExists(string email)
        {
            try
            {
                bool result = userBL.DoesEmailExist(email);

                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "email exists" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "email doesn't exist" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Get-all-ussers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserEntity> result = userBL.GetAllUsers();

                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<UserEntity>> { Status = true, Message = "users retrieved successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "No user found" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgetPassword(string Email, string Token)
        {
            try
            {
                string result =await userBL.ForgetPassword(Email,Token);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "forget password" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Reset-Password")]
        public IActionResult ResetPassword(resetPassword reset)
        {
            try
            {
                string email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var result = userBL.ResetPassword(email, reset);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "reset password" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetUserByFirstName")]
        public IActionResult GetDetailsOfUser(string FirstName)
        {
            try
            {
                List<UserEntity> result = userBL.GetDetailsOfUser(FirstName);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "Fetched Details" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Details Not fetched" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login-with-jwt")]
        public IActionResult LoginWithJwt(UserLogin userLogin)
        {
            try
            {
                var result = userBL.LoginWithJwt(userLogin);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "login successfully with token", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Error" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Fetch-User")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var result = userBL.GetUserById(userId);

                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "fetched the data", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "didnt fetch the data" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("Update-User")]
        public IActionResult UpdateUser(long userId, UserRegistration userRegistration)
        {
            try
            {
                bool result = userBL.UpdateUser(userId, userRegistration);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "updated user successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update user" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
