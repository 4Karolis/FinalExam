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
    }
}
