using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL: IUserInterfaceRL
    {
        private readonly FundooContext fundooContext;

        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
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
                if(result > 0)
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
                UserEntity userEntity = fundooContext.Users.FirstOrDefault(a => a.Email == userLogin.Email );
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
    }
}
