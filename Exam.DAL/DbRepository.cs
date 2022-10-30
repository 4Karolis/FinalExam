using Exam.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DAL
{
    public class DbRepository : IDbRepository
    {
        private readonly ExamDbContext _dbContext;
        public DbRepository(ExamDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
        public async Task InsertAccountAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Image> AddImageAsync(Image image)
        {
            await _dbContext.Images.AddAsync(image);
            return image;
        }
        public async Task<Image> GetImageAsync(int id)
        {
            return await _dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.Include(x => x.PersonalInfo).Include(x => x.PersonalInfo.ResidentialInfo).
                FirstOrDefaultAsync(u => u.Id == id);
        }
       
        public async Task<PersonalInfo> GetPersonalInfoAsync(int userId)
        {
            return await _dbContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
        }
        public async Task<ResidentialInfo> GetResidentialInfoAsync(int personalInfoId)
        {
            return await _dbContext.ResidentialInfos.FirstOrDefaultAsync(r => r.PersonalInfoId == personalInfoId);
        }
        public async Task ChangeName(int userId, string name)
        {
            var existingUser = await _dbContext.Users.Include(x => x.PersonalInfo).FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.Name = name;
            //_dbContext.Users.Attach(user);
            //SaveChangesAsync();
        }
        public async Task ChangeLastnameAsync(int userId, string lastname)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.Lastname = lastname;
        }
    }
}
