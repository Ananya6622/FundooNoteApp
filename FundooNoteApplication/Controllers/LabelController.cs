using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Linq;
using System;
using RepositoryLayer.Service;
using System.Collections.Generic;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [Authorize]
        [HttpPost("Add-Label")]
        public IActionResult AddLabel(LabelModel labelModel, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = labelBL.AddLabel(labelModel, noteId,userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelEntity> { Status = true, Message = "added label successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to add label" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("Get-All-Labels")]
        public IActionResult GetAllLabels(long labelId, long UserId, long noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                List<LabelEntity> result = labelBL.GetAllLabels(labelId,userId,noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<List<LabelEntity>> { Status = true, Message = "retrieved all labels",Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "unable to get labels" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("Update-label")]
        public IActionResult updateLabel(long labelId, long noteId, long userId, LabelModel labelModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = labelBL.UpdateLabel(labelId,noteId, userId, labelModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<bool> { Status = true, Message = "updated label successfully",Data = result });
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
        [HttpDelete("delete-label")]
        public IActionResult deleteLabel(long labelId, long noteId, long userId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                bool result = labelBL.DeleteLabel(labelId,noteId, userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<bool> { Status = true, Message = "deleted note successfully",Data = result });
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
