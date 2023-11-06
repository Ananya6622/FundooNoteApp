using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL:ICollabInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public CollabRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public CollabEntity AddCollab(CollabModel collabModel,long noteId,long userId)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.CollabEmail = collabModel.CollabEmail;
                UserEntity user = fundooContext.Users.FirstOrDefault(x => x.UserId == userId);
                NoteEntity note = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                collabEntity.Users = user;
                collabEntity.Notes = note;
                fundooContext.Collabs.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabEntity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabEntity> GetAllCollabs()
        {
            try
            {
                return fundooContext.Collabs.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCollab(long collabId,long noteId,long userId)
        {
            try
            {
                var result = fundooContext.Collabs.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.CollabId == collabId);
                if(result != null)
                {
                    fundooContext.Collabs.Remove(result);
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
