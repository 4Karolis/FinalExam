using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DAL
{
    public interface IDbRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task InsertAccountAsync(User user);
        Task SaveChangesAsync();
        Task AddImageAsync(Image image);
        Task<Image> GetImageAsync(int id);

    }
}
