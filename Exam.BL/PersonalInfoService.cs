﻿using Exam.DAL;
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

        public async Task ChangeName(int userId, string name)
        {
            await _dbRepository.ChangeName(userId, name);
            await _dbRepository.SaveChangesAsync();
        }
        public async Task ChangeLastnameAsync(int userId, string lastname)
        {
            await _dbRepository.ChangeLastnameAsync(userId, lastname);
            await _dbRepository.SaveChangesAsync();
        }

        public async Task<PersonalInfo> GetPersonalInfoAsync(int userId)
        {
            return await _dbRepository.GetPersonalInfoAsync(userId);
        }
    }
}
