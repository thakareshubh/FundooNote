using BuisnessLayer.Interface;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class LabelController : ControllerBase
    {
        FundooDbContext fundooDBContext;
        ILabelBl labelBl;
        public LabelController(ILabelBl labelBl, FundooDbContext fundooContext)
        {
            this.labelBl = labelBl;
            this.fundooDBContext = fundooContext;
        }

        //AddLabale
        [Authorize]
        [HttpPost("AddLabel")]
        public async Task<IActionResult> AddLabel(int noteId, string LabelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.labelBl.AddLabel(UserId, noteId, LabelName);
                return this.Ok(new { success = true, message = "Label Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPost("CreateLabel")]
        public async Task<IActionResult> createLabel(lablePostModel lablePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.labelBl.createLabel(UserId,   lablePostModel );
                return this.Ok(new { success = true, message = "Label created Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //  [Authorize]
        //[HttpGet("GetLabelByuserId/{userId}")]
        //public async Task<ActionResult> GetLabelByuserId(int userId)
        //{
        //    try
        //    {
        //        List<RepositoryLayer.Entity.Label> list = new List<RepositoryLayer.Entity.Label>();
        //        var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
        //        int userID = Int32.Parse(userid.Value);
        //        list = await this.labelBl.GetLabelByuserId(userId);
        //        if (list == null)
        //        {
        //            return this.BadRequest(new { success = false, message = "Failed to get label" });
        //        }
        //        return this.Ok(new { success = true, message = $"Label get successfully", data = list });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [Authorize]
        [HttpDelete("DeleteLabel/{LabelId}")]
        public async Task<ActionResult> DeleteLabel(int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBl.DeleteLabel(LabelId, userId);
                return this.Ok(new { success = true, message = $"Label Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateLabel/{LabelId}/{LabelName}")]
        public async Task<ActionResult> UpdateLabel(string LabelName ,int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.labelBl.UpdateLabel(userId, LabelId, LabelName);
                if (result == null)
                {
                    return this.BadRequest(new { success = true, message = "Updation of Label failed" });
                }
                return this.Ok(new { success = true, message = $"Label updated successfully", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("Getlabel")]
        public async Task<ActionResult> GetLabel()
        {
            try
            {
                List<Label> list = new List<Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                list = await this.labelBl.Getlabel(userId);
                if (list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
