using Exam.Domain;

namespace Exam.BL
{
    public interface IPersonalInfoService
    {
        Task<PersonalInfo> GetPersonalInfoAsync(int userId);
        Task ChangeNameAsync(int userId, string name);
        Task ChangeLastnameAsync(int userId, string lastname);
        Task ChangePersonalCodeAsync(int userId, string personalCode);
        Task ChangePhoneAsync(int userId, string phone);
        Task ChangeEmailAsync(int userId, string email);
    }
}
