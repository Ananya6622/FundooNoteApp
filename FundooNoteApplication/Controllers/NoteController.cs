using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<UserController> logger;
        public NoteController(INoteBL noteBL, IDistributedCache distributedCache, ILogger<UserController> logger)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }
        //[Authorize]
        [HttpPost("Add-Note")]
        public IActionResult AddNote(NoteModel noteModel,long UserId)
        {
            try
            {
                HttpContext.Session.GetInt32("UserID");
                //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
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
                    return this.Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "retrieved all notes", Data = result });
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
        [HttpGet("Get-all-notes-using-reddis")]
        public async Task<IActionResult> GetAllNotesUsingRedis(int userId)
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedisNoteList = await distributedCache.GetAsync(CacheKey);
                if (RedisNoteList != null)
                {
                    logger.LogDebug("setting the list to cache which is requested for the first time");
                    var SerializedNoteList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerializedNoteList);

                }
                else
                {
                    logger.LogDebug("setting a list to cache which is requested for the first time");
                    NoteList = (List<NoteEntity>)noteBL.GetAllNotes(userId);
                    var SeralizedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SeralizedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(CacheKey, redisNoteList, options);

                }
                logger.LogInformation("got all notes list successfully from redis");
                return Ok(NoteList);

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "exception thrown.. ");
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("search-note-by-date")]
        public IActionResult searchNotes(DateTime createdAt)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = noteBL.SearchNote(createdAt);
                if(result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "searched note", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "note doesnt exist" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("search-note-by-title")]
        public IActionResult searchNotesByTitle(string title)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<NoteEntity> result = noteBL.SearchNoteByTitle(title);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "searched note by title", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "note doesnt exist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = ex.Message });
            }
        }
    }
 }

