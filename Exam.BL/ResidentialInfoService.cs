﻿using Exam.DAL;
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
        public async Task ChangeStreetAsync(int userId, string street)
        {
            await _dbRepository.ChangeStreetAsync(userId, street);
            await _dbRepository.SaveChangesAsync();
        }
        public async Task ChangeHouseNumberAsync(int userId, string houseNumber)
        {
            await _dbRepository.ChangeHouseNumberAsync(userId, houseNumber);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
