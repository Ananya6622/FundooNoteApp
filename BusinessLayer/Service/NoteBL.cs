using BusinessLayer.Interface;
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
    }
}
