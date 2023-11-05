using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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
        [Authorize]
        [HttpPost("Add-Note")]
        public IActionResult AddNote(NoteModel noteModel,long UserId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        [Authorize]
        [HttpGet("Get-All-Notes")]
        public IActionResult GetAllNotes(long UserId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        [Authorize]
        [HttpPut("Update-note")]
        public IActionResult updateNotes( long noteId, long userId, NoteModel noteModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        [Authorize]
        [HttpDelete("delete-note")]
        public IActionResult deleteNote(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
        [Authorize]
        [HttpGet("Is-pin-or-not")]
        public IActionResult IsPinOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = noteBL.IsPinOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "pinned" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not pinned" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Is-Archieve-or-not")]
        public IActionResult IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = noteBL.IsArchieveOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "archieved" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not archieve" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("Is-trash-or-not")]
        public IActionResult IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = noteBL.IsTrashOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "is in trash" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "not in trash" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("color")]
        public IActionResult color(long noteId, string color)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = noteBL.color(noteId, color);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "updated color successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update color" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("Reminder")]
        public IActionResult Reminder(long noteId,DateTime remind)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = noteBL.Reminder(noteId, remind);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "updated reminder successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update reminder" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteId, long userId, IFormFile img)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = noteBL.UploadImage(noteId,userId,img );
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "updated image successfully"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to update image" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Fetch-note")]
        public IActionResult GetNoteById(long noteId,long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                NoteEntity result = noteBL.GetNoteById(noteId,userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NoteEntity> { Status = true, Message = "retrieved all notes" , Data = result});
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
    }
}
