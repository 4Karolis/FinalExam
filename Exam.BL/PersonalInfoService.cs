using Exam.DAL;
using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IDbRepository _dbRepository;
        public PersonalInfoService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public Task ChangeName(int userId, string name)
        {
            var user = await _dbRepository.GetUserByIdAsync(userId);
            user.PersonalInfo.Name = name;

            _dbRepository.ChangeName(userId, name);
            _dbRepository.SaveChangesAsync();
        }

        public async Task<PersonalInfo> GetPersonalInfoAsync(int userId)
        {
            return await _dbRepository.GetPersonalInfoAsync(userId);
        }
    }
}
