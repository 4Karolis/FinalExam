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
        Task<Image> AddImageAsync(Image image);
        Task<Image> GetImageAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<PersonalInfo> GetPersonalInfoAsync(int userId);
        Task<ResidentialInfo> GetResidentialInfoAsync(int personalInfoId);

    }
}
