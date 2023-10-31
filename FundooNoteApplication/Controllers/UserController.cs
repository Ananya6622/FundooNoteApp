using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.UserRegistrations(userRegistration);
                if(result!= null)
                {
                    return this.Ok(new ResponseModel<UserEntity> { Status = true, Message = "User Registration successfull", Data = result });
                }
                else
                {
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
        

    }
}
