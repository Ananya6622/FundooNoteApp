﻿using BusinessLayer.Interface;
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
                if(result!= null)
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
            catch(Exception ex)
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
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "email exists"});
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
                    return this.Ok(new ResponseModel<List<UserEntity>> { Status = true, Message = "users retrieved successfully", Data = result }) ;
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
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                string result = userBL.ForgetPassword(Email);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "forget password"});
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
                if(result!= null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "Fetched Details" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Details Not fetched" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
