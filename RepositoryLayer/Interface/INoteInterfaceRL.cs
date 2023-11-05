using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteInterfaceRL
    {
        public NoteEntity AddNote(NoteModel noteModel, long userId);
        public List<NoteEntity> GetAllNotes(long UserId);
        public bool UpdateNote(long noteId, long userId, NoteModel noteModel);
        public bool DeleteNode(long noteId, long userId);
        public bool IsPinOrNot(long noteId, long userId);
        public bool IsArchieveOrNot(long noteId, long userId);
        public bool IsTrashOrNot(long noteId, long userId);
        public NoteEntity color(long noteId, string color);
        public NoteEntity Reminder(long noteId, DateTime remind);

        public string UploadImage(long noteId, long userId, IFormFile img);
        public NoteEntity GetNoteById(long noteId, long userId);
    }
}
