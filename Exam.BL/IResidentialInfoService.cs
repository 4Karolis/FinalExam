﻿using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public interface IResidentialInfoService
    {
        Task<ResidentialInfo> GetResidentialInfoAsync(int personalInfoId);
        Task ChangeCityAsync(int userId, string city);
        Task ChangeStreetAsync(int userId, string street);
        Task ChangeHouseNumberAsync(int userId, string houseNumber);
        Task ChangeApartmentNumberAsync(int userId, string apartmentNumber);
    }
}
