using CommonLayer.Users;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface InoteRl
    {

        Task AddNote(int userId, NoteModel noteModel);
        bool DeleteNote(int noteId);
        Task ChangeColor(int userId, int noteId, string color);
        Task ArchiveNote(int userId, int noteId);
        Task<Note> UpdateNote(int noteId, NoteUpDateModel noteUpdateModel);
        Task<Note> GetNote(int noteId);
        Task<Note> TrashNote(int userId, int noteId);
    }  
}
