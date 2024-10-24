using LibraryManagementModels.Entities;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using LibraryManagementService.Service;
using Microsoft.Extensions.DependencyInjection;

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

            // Repository registrations
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<ILibraryBRepository, LibraryBRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            // Audit trial registration
            services.AddScoped<IRepository<StudentAuditTrial>, Repository<StudentAuditTrial>>();
            services.AddScoped<IRepository<StudentSubCourseAuditTrial>, Repository<StudentSubCourseAuditTrial>>();

            //direct repository call for no business layer . Like child objects
            services.AddScoped<IRepository<StudentSubCourse>, Repository<StudentSubCourse>>();

           

            // AutoMapper registration
            services.AddAutoMapper(typeof(MappingProfile)); // Registers all profiles including MappingProfile


            return services;
        }
    }
}
