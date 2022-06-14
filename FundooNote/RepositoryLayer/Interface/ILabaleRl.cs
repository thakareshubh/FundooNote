using CommonLayer.Users;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabaleRl
    {
        Task AddLabel(int userId, int noteId, string labelName);
        Task createLabel(int userId, lablePostModel lablePostModel);
        

        Task DeleteLabel(int LabelId, int UserId);
        Task<Label> UpdateLabel(int UserId, int LabelId, string LabelName);
        Task<List<Label>> Getlabel(int UserId);

    }
}

