using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]



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
      }
}
