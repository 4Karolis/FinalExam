using Exam.DAL;
using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public class ResidentialInfoService : IResidentialInfoService
    {
        private readonly IDbRepository _dbRepository;
        public ResidentialInfoService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        public async Task<ResidentialInfo> GetResidentialInfoAsync(int personalInfoId)
        {
            return await _dbRepository.GetResidentialInfoAsync(personalInfoId);
        }
        public async Task ChangeCityAsync(int userId, string email)
        {
            await _dbRepository.ChangeCityAsync(userId, email);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
