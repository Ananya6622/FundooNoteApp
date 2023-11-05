using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Account;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Account = CloudinaryDotNet.Account;

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
                if(result.Istrash == true)
                {
                    fundooContext.Notes.Remove(result);
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Istrash = true;
                    this.fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsPinOrNot(long noteId, long userId)
        {
            try
            {
                NoteEntity result = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.IsPin == true)
                    {
                        result.IsPin = false;
                        this.fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.IsPin = true;
                        this.fundooContext.SaveChanges();
                        return true;
                    }
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

        public bool IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                NoteEntity result = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.IsArchive == true)
                    {
                        result.IsArchive = false;
                        this.fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.IsArchive = true;
                        this.fundooContext.SaveChanges();
                        return true;
                    }
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

        public bool IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                NoteEntity result = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    if (result.Istrash == true)
                    {
                        result.Istrash = false;
                        this.fundooContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.Istrash = true;
                        this.fundooContext.SaveChanges();
                        return true;
                    }
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
        public NoteEntity color(long noteId, string color)
        {
            try
            {
                NoteEntity note = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (note.color != null)
                {
                    note.color = color;
                    this.fundooContext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity Reminder(long noteId, DateTime remind)
        {
            try
            {
                NoteEntity note = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (note.Remainder != null)
                {
                    note.Remainder = remind;
                    this.fundooContext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UploadImage(long noteId,long userId,IFormFile img)
        {
            try
            {
                var result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (result != null)
                {
                    Account account = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                        this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadReult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadReult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image uploaded succesfully";
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

        public NoteEntity GetNoteById(long noteId,long userId)
        {
            try
            {
                NoteEntity noteEntity = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                return noteEntity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
