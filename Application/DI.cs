using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.Services.Cloudinary;
using Application.Services.Drive;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAssessmentService, AssessmentService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IJobTrackingService, JobTrackingService>();
            services.AddScoped<IUserCvService, UserCvService>();

            services.AddScoped<ICvAnalysisService, CvAnalysisService>();
            //services.AddSingleton<IGoogleDriveService, GoogleDriveService>();
            services.AddSingleton<IStorageService, CloudinaryStorageService>();
            return services;
        }
    }
}
