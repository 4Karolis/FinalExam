﻿using Exam.Domain;
using Microsoft.EntityFrameworkCore;


namespace Exam.DAL
{
    public class ExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; } 
        public DbSet<ResidentialInfo> ResidentialInfos { get; set; }
        public DbSet<Image> Images { get; set; }

        public ExamDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}