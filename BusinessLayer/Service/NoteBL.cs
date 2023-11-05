using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL: INoteBL
    {
        private readonly INoteInterfaceRL noteInterfaceRL;

        public NoteBL(INoteInterfaceRL noteInterfaceRL)
        {
            this.noteInterfaceRL = noteInterfaceRL;
        }

        public NoteEntity AddNote(NoteModel noteModel, long userId)
        {
            try
            {
                return this.noteInterfaceRL.AddNote(noteModel, userId);
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
                return noteInterfaceRL.GetAllNotes(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateNote(long noteId, long userId, NoteModel noteModel)
        {
            try
            {
                return noteInterfaceRL.UpdateNote(noteId, userId, noteModel);
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
                return noteInterfaceRL.DeleteNode(noteId, userId);
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
                return noteInterfaceRL.IsPinOrNot(noteId, userId);
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
                return noteInterfaceRL.IsArchieveOrNot(noteId, userId);
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
                return noteInterfaceRL.IsTrashOrNot(noteId, userId);
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
                return noteInterfaceRL.color(noteId, color);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity Reminder(long noteId, DateTime remind)
        {
            try
            {
                return noteInterfaceRL.Reminder(noteId, remind);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                return noteInterfaceRL.UploadImage(noteId, userId, img);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity GetNoteById(long noteId, long userId)
        {
            try
            {
                return noteInterfaceRL.GetNoteById(noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
