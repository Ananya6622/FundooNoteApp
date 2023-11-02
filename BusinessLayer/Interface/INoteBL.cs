using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity AddNote(NoteModel noteModel, long userId);
        public List<NoteEntity> GetAllNotes(long UserId);
        public bool UpdateNote(long noteId, long userId, NoteModel noteModel);
        public bool DeleteNode(long noteId, long userId);
    }
}
