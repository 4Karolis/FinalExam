using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.DAL.Extensios
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExamDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IDbRepository, DbRepository>();

            return services;
        }
    }
}
