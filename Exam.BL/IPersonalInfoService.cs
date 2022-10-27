using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public interface IPersonalInfoService
    {
         Task<PersonalInfo> GetPersonalInfoAsync(int userId);
        Task ChangeName(int userId, string name);

    }
}
