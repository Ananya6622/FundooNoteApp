using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL:ILabelInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public LabelEntity AddLabel(LabelModel labelModel,long noteId,long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.LabelName = labelModel.LabelName;
                UserEntity user = fundooContext.Users.FirstOrDefault(x => x.UserId == userId);
                NoteEntity note = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                labelEntity.UsersTable = user;
                labelEntity.Notes = note;
                fundooContext.Labels.Add(labelEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
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
        public List<LabelEntity> GetAllLabels(long labelId,long UserId,long noteId)
        {
            try
            {
                List<LabelEntity> result = fundooContext.Labels.Where(x => x.UserId == UserId && x.NoteId == noteId && x.LabelId == labelId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateLabel(long labelId,long noteId, long userId, LabelModel labelModel)
        {
            try
            {
                var result = fundooContext.Labels.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId);
                if (result != null)
                {
                    result.LabelName = labelModel.LabelName;
                    fundooContext.SaveChanges();
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
        public bool DeleteLabel(long labelId,long noteId, long userId)
        {
            try
            {
                var result = fundooContext.Labels.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId && x.LabelId == labelId);
                if (result != null)
                {
                    fundooContext.Labels.Remove(result);
                    fundooContext.SaveChanges();
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
    }
}
