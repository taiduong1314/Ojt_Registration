using API.Middleware;
using Application.Interface;
using Application.OjtReport;
using Application.Recruitment_Informations;
using Application.Students;
using FluentValidation.AspNetCore;
using Infrastructure.Firebase;
using Infrastructure.Hasing;
using Infrastructure.JWTGenerate;
using Infrastructure.PdfSupport;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //bình thường ở đây chỉ có controller thôi, để add và sử dụng fluent validator mình add thêm fluence validator vào.
            services.AddControllers()
                .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<EvaluateStudent>();
            });

            //add cross origin
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //Link to Database services
            services.AddDbContext<DataContext>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add Services for MediatR
            services.AddMediatR(typeof(StudentInfo.Handler).Assembly);

            //Add Scope for generate firebase token
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            //Add Scope for file process
            services.AddScoped<IPdfFileSupport, PdfFileSupport>();

            //Add Scope for process firebase token
            services.AddScoped<IFirebaseSupport, FirebaseSupport>();

            //Add Scope for hasing password
            services.AddScoped<IHasingSupport, HasingSupport>();

            //Add verify jwt services
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secretkey"]))
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = tokenValidationParams;
                });

            //services.AddMvc(opt =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    opt.Filters.Add(new AuthorizeFilter(policy));
            //});

            //add services note in parameter in swagger
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Ojt_Registration API",
                        Description = "For get information of ojt-registration app",
                        Version = "v1.0"
                    });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, fileName);
                opt.IncludeXmlComments(path);
            });

            //add services process pdf file
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory)));

            //redirect service
            services.AddHttpsRedirection(opt =>
            {
                opt.HttpsPort = 5001;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseDeveloperExceptionPage();
            app.UseSwagger();


            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ojt_Registration API"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
