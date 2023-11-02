using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL: INoteInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NoteRL(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public NoteEntity AddNote(NoteModel noteModel, long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = noteModel.Title;
                noteEntity.Note = noteModel.Note;
                noteEntity.Remainder = noteModel.Remainder;
                noteEntity.color = noteModel.color;
                noteEntity.Image = noteModel.Image;
                noteEntity.IsArchive = noteModel.IsArchive;
                noteEntity.IsPin = noteModel.IsPin;
                noteEntity.Istrash = noteModel.Istrash;
                noteEntity.Createat = noteModel.Createat;
                noteEntity.Updateat = noteModel.Updateat;
                UserEntity user = fundooContext.Users.FirstOrDefault(x => x.UserId== userId);
                noteEntity.Users = user;
                fundooContext.Notes.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                   
                    return noteEntity;
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

        public List<NoteEntity> GetAllNotes(long UserId)
        {
            try
            {
                List<NoteEntity> result = fundooContext.Notes.Where(x => x.UserId == UserId).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateNote(long noteId,long userId,NoteModel noteModel)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(x => x.UserId ==userId && x.NoteId == noteId);
                if(result != null)
                {
                    result.Title = noteModel.Title;
                    result.Note = noteModel.Note;
                    result.Remainder = noteModel.Remainder;
                    result.color = noteModel.color;
                    result.Image = noteModel.Image;
                    result.IsArchive = noteModel.IsArchive;
                    result.IsPin = noteModel.IsPin;
                    result.Istrash = noteModel.Istrash;
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
        public bool DeleteNode(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
                if(result != null)
                {
                    fundooContext.Notes.Remove(result);
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
