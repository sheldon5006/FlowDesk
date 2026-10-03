using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;
using FlowDesk.Api.Services;
using FlowDesk.Api.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace FlowDesk.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlowDeskServices(
            this IServiceCollection services)
        {
            // Generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Application services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAvailabilityService, AvailabilityService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<ISchedulingService, SchedulingService>();
            services.AddScoped<IDashboardService, DashboardService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserService, UserService>();

            // User & authentication
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IWarehouseService, WarehouseService>();

            services.AddScoped<IWarehouseLocationService,WarehouseLocationService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
