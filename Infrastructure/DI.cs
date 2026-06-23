using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("dbConStr")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpClient<IAssessmentClient, AssessmentClient>(client =>
            {
                var baseUrl = configuration["AssessmentEngine:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + "/");
            } );

            services.AddHttpClient<ICvAnalyzerClient, CvAnalyzerClient>(client =>
            {
                var baseUrl = configuration["CvAnalyzer:BaseUrl"];
                client.BaseAddress = new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + '/');
                client.Timeout = TimeSpan.FromSeconds(120);
            });

            services.AddHttpClient<IInterviewSimulatorClient, InterviewSimulatorClient>(client =>
            {
                var baseUrl = configuration["InterviewSimulator:BaseUrl"]!;
                client.BaseAddress = new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + '/');
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            return services;
        }
    }
}
