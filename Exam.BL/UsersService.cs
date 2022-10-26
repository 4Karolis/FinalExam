﻿using DTOs;
using Exam.DAL;
using Exam.Domain;
using FinalExam.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public class UsersService : IUsersService
    {
        private readonly IDbRepository _dbrepository;
        public UsersService(IDbRepository dbrepository)
        {
            _dbrepository = dbrepository;
        }
        public async Task<bool> CreateUserAsync(string username, string password, PersonalInfoDto personalInfo, ResidentialInfoDto residentialInfo, Image image)
        {
            var existingUser = await _dbrepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return false;
            }
            var (hash, salt) = CreatePasswordHash(password);
            var newUser = new User
            {
                Username = username,
                //Password = password,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "user",
                PersonalInfo = new PersonalInfo
                {
                    Name = personalInfo.Name,
                    Lastname = personalInfo.Lastname,
                    PersonalCode = personalInfo.PersonalCode,
                    Phone = personalInfo.Phone,
                    Email = personalInfo.Email,
                    ProfilePic = image,
                    //{
                    //    //        public IFormFile ProfilePic { get; set; }
                    //    ContentType = imageDto.ProfilePic.ContentType
                    //    //ImageBytes = imageDto.ProfilePic.

                    //},
                    ResidentialInfo = new ResidentialInfo
                    {
                        City = residentialInfo.City,
                        StreetName = residentialInfo.StreetName,
                        HouseNumber = residentialInfo.HouseNumber,
                        ApartmentNumber = residentialInfo.ApartmentNumber
                    }
                },
            };
            await _dbrepository.InsertAccountAsync(newUser);
            await _dbrepository.SaveChangesAsync();

            return true;
        }
        public async Task<(bool authenticationSuccessful, User? user)> LoginAsync(string username, string password)
        {
            var account = await _dbrepository.GetUserByUsernameAsync(username);
            if (account == null)
            {
                return (false, null);
            }
            if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return (true, account);
            }
            else
            {
                return (false, null);
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (hash, salt);
        }
    }
}
