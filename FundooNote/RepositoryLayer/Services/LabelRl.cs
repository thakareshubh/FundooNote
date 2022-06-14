using CommonLayer.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRl:ILabaleRl
    {
        FundooDbContext fundooDBContext;
        public IConfiguration configuration { get; }

        public LabelRl(FundooDbContext fundooDBContext, IConfiguration configuration)
        {
            this.fundooDBContext = fundooDBContext;
            this.configuration = configuration;
        }
        public async Task AddLabel(int userId, int noteId, string LabelName)
        {
            try
            {
                Label label = new Label();
                label.UserId = userId;
                label.NoteId = noteId;
                label.LabelName = LabelName;

                fundooDBContext.Add(label);
                await fundooDBContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task createLabel(int userId, lablePostModel lablePostModel)
        {
            try
            {
                Label label = new Label();
                label.UserId = userId;

                label.LabelName = lablePostModel.lableName;

                fundooDBContext.Add(label);
                await fundooDBContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public async Task<List<Entity.Label>> Getlabel(int UserId)
        {
            try
            {
                List<Entity.Label> reuslt = await fundooDBContext.Label.Where(u => u.UserId == UserId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entity.Label> UpdateLabel(int UserId, int LabelId, string LabelName)
        {
            try
            {

                Entity.Label reuslt = fundooDBContext.Label.FirstOrDefault(u => u.LabelId == LabelId && u.UserId == UserId);

                if (reuslt != null)
                {
                    reuslt.LabelName = LabelName;
                    await fundooDBContext.SaveChangesAsync();
                    
                    return reuslt;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLabel(int LabelId, int UserId)
        {
            try
            {
                var result = fundooDBContext.Label.FirstOrDefault(e => e.LabelId == LabelId && e.UserId == UserId);
                fundooDBContext.Label.Remove(result);
                if (result != null)
                {
                    fundooDBContext.Remove(result);
                    await fundooDBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
