using LibraryManagementModels.BusinessModels;
using LibraryManagementModels.Entities;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using LibraryManagementService.Service;

namespace LibraryManagement.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            // Service registrations
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILibraryBService, LibraryBService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICourseService, CourseService>();

            // Audit trial registration
            services.AddScoped<IAuditTrialBaseRepository<StudentAuditTrial>, AuditTrialBaseRepository<StudentAuditTrial>>();

            // Repository registrations
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<ILibraryBRepository, LibraryBRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            // AutoMapper registration
            services.AddAutoMapper(typeof(MappingProfile)); // Registers all profiles including MappingProfile


            return services;
        }
    }
}
