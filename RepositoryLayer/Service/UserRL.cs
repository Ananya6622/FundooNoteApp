using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public UserEntity UserRegistrations(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.Email = userRegistration.Email;
                userEntity.Password = EncryptPass(userRegistration.Password);
                fundooContext.Users.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string EncryptPass(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public UserEntity UserLogins(UserLogin userLogin)
        {
            try
            {
                UserEntity userEntity = fundooContext.Users.FirstOrDefault(a => a.Email == userLogin.Email);
                UserEntity userEntityy = fundooContext.Users.FirstOrDefault(a => a.Password == userLogin.Password);

                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DoesEmailExists(string email)
        {
            return fundooContext.Users.Any(u => u.Email == email);
        }

        public List<UserEntity> GetAllUsers()
        {
            return fundooContext.Users.ToList();
        }

        public string GenerateToken(string Email, int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim("Email", Email),
               new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<string> ForgetPassword(string Email, string Token)
        {
            try
            {
                //HttpClient client = new HttpClient();
                //client.GetAsync("http://localhost:7164/api/Function1");
                //MessageService messageService = new MessageService();
                //await messageService.SendMessageQueue(Email,Token);
                var result = fundooContext.Users.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    //MSMQModel mSMQModel = new MSMQModel();
                    //mSMQModel.SendMessage(token, result.Email, result.FirstName);
                    MessageService messageService = new MessageService();
                    await messageService.SendMessageQueue(Email, Token);
                    var postData = new
                    {
                        Token = token,
                        Email = result.Email 
                    };

                    
                    var jsonData = JsonConvert.SerializeObject(postData);

                    
                    var functionUrl = "http://localhost:7164/api/Function1";
                    using (var client = new HttpClient())
                    {
                        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(functionUrl, content);

                        
                        if (response.IsSuccessStatusCode)
                        {
                            return "Password reset request sent successfully";
                        }
                        else
                        {
                            return "Failed to send password reset request";
                        }
                    }

                    return token.ToString();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, resetPassword reset)
        {
            try
            {
                if (reset.Password.Equals(reset.ConfirmPassword))
                {
                    var user = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = reset.Password;
                    fundooContext.SaveChanges();
                    //MessageService messageService = new MessageService();
                    //messageService.SendMessageQueue();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //take input of username and print the details of that user. else throw msg user does not exist
        public List<UserEntity> GetDetailsOfUser(string firstName)
        {
            try
            {
                List<UserEntity> userEntity = fundooContext.Users.Where(x => x.FirstName == firstName).ToList();
                return userEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string LoginWithJwt(UserLogin userLogin)
        {
            try
            {
                var EncodedPassword = EncryptPass(userLogin.Password);
                UserEntity result = fundooContext.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == EncodedPassword);
                if (result != null)
                {
                    var token = this.GenerateToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserTicket CreateTicketForPassword(string emailId, string token)
        {
            try
            {
                var UserDetails = fundooContext.Users.FirstOrDefault(x => x.Email == emailId);
                if (UserDetails != null)
                {
                    UserTicket ticketResponse = new UserTicket
                    {

                        FirstName = UserDetails.FirstName,
                        LastName = UserDetails.LastName,
                        EmailId = emailId,
                        Token = token,
                        IssueAt = DateTime.Now
                    };
                    return ticketResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     
        
        public UserEntity GetUserById(int userId)
        {
            try
            {
                UserEntity userEntity = fundooContext.Users.FirstOrDefault(x => x.UserId == userId);
                return userEntity;
            }
            catch(Exception ex)
            {
                throw ex;
            }  
        }

        public bool UpdateUser(long userId,UserRegistration userRegistration)
        {
            try
            {
                var result = fundooContext.Users.FirstOrDefault(x => x.UserId == userId);
                if (result != null)
                {
                    result.FirstName = userRegistration.FirstName;
                    result.LastName = userRegistration.LastName;
                    result.Email = userRegistration.Email;
                    result.Password = userRegistration.Password;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
