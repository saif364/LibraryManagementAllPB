using LibraryManagementModels.Entities;
using LibraryManagementRepository.DbConfigure;
using LibraryManagementRepository.InterfaceRepository;
using LibraryManagementRepository.Repository;
using LibraryManagementService.InterfaceService;
using LibraryManagementService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServiceCollectionsDI(this IServiceCollection services, string connectionString)
        {

            #region Program cs configure services
            // Register LibraryDbContext with Identity
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Identity with the LibraryDbContext
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            });
            //

            services.AddControllersWithViews();
            //swagger for api
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //

            // Configure cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            //
            #endregion


            #region  Own Service and auto mapper 
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
            services.AddScoped<IRepository<StudentSubAttachmentAuditTrial>, Repository<StudentSubAttachmentAuditTrial>>();

            //direct repository call for no business layer . Like child objects
            services.AddScoped<IRepository<StudentSubCourse>, Repository<StudentSubCourse>>();
            services.AddScoped<IRepository<StudentSubAttachment>, Repository<StudentSubAttachment>>();

             
            #endregion

            // AutoMapper registration
            services.AddAutoMapper(typeof(MappingProfile)); // Registers all profiles including MappingProfile



            return services;
        }
    }
}
