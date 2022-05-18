using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabaleRl
    {
        Task AddLabel(int userId, int noteId, string labelName);
    }
}

