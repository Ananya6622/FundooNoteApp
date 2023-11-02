using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [HttpPost("Add-Note")]
        public IActionResult AddNote(NoteModel noteModel,long UserId)
        {
            try
            {
                var result = noteBL.AddNote(noteModel, UserId);
                if(result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "added note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add Note" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("Get-All-Notes")]
        public IActionResult GetAllNotes(long UserId)
        {
            try
            {
                List<NoteEntity> result = noteBL.GetAllNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "retrieved all notes" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get Notes" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut("Update-note")]
        public IActionResult updateNotes( long noteId, long userId, NoteModel noteModel)
        {
            try
            {
                bool result = noteBL.UpdateNote(noteId, userId, noteModel);
                if(result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "updated note successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update Note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("delete-note")]
        public IActionResult deleteNote(long noteId, long userId)
        {
            try
            {
                bool result = noteBL.DeleteNode(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "deleted note successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to delete Note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
