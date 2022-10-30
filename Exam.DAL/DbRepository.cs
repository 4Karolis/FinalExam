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
            return await _dbContext.Users.Include(x => x.PersonalInfo).Include(x => x.PersonalInfo.ProfilePic).Include(x => x.PersonalInfo.ResidentialInfo).
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
        public async Task ChangeNameAsync(int userId, string name)
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
        public async Task ChangePersonalCodeAsync(int userId, string personalCode)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.PersonalCode = personalCode;
        }
        public async Task ChangePhoneAsync(int userId, string phoneNumber)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.Phone = phoneNumber;
        }
        public async Task ChangeEmailAsync(int userId, string email)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.Email = email;
        }

        public async Task ChangeCityAsync(int userId, string city)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).Include(u => u.PersonalInfo.ResidentialInfo)
            .FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.ResidentialInfo.City = city;
        }

        public async Task ChangeStreetAsync(int userId, string street)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).Include(u => u.PersonalInfo.ResidentialInfo)
            .FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.ResidentialInfo.StreetName = street;
        }

        public async Task ChangeHouseNumberAsync(int userId, string houseNumber)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).Include(u => u.PersonalInfo.ResidentialInfo)
            .FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.ResidentialInfo.HouseNumber = houseNumber;
        }

        public async Task ChangeApartmentNumber(int userId, string apartmentNumber)
        {
            var existingUser = await _dbContext.Users.Include(u => u.PersonalInfo).Include(u => u.PersonalInfo.ResidentialInfo)
            .FirstOrDefaultAsync(u => u.Id == userId);
            existingUser.PersonalInfo.ResidentialInfo.ApartmentNumber = apartmentNumber;
        }
    }
}
