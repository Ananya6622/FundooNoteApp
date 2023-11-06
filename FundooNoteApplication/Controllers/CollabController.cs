using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Linq;
using System;
using BusinessLayer.Service;
using System.Collections.Generic;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("Add-collab")]
        public IActionResult AddCollab(CollabModel collabModel, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = collabBL.AddCollab(collabModel,noteId,userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CollabEntity> { Status = true, Message = "added collab successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add collab" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Get-All-Collabs")]
        public IActionResult GetAllCollabs()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<CollabEntity> result = collabBL.GetAllCollabs();
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<CollabEntity>> { Status = true, Message = "retrieved all Collabs", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get Collabs" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("delete-collab")]
        public IActionResult deletecollab(long collabId, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = collabBL.DeleteCollab(collabId, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<bool> { Status = true, Message = "deleted collab successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to delete collab" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
