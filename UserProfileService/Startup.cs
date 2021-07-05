using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserProfileService.Data;
using UserProfileService.Data.Repository;
using UserProfileService.Domain.Commands;
using UserProfileService.Domain.Interfaces;
using UserProfileService.Domain.Query;
using UserProfileService.Domain.Services;
using UserProfileService.Mapping;

namespace UserProfileService
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
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>();
                    fv.RegisterValidatorsFromAssemblyContaining<UpdateUserCommand>();
                    fv.RegisterValidatorsFromAssemblyContaining<GetUsersQuery>();
                });

            services.AddDbContext<UserProfileDbContext>(
                op => op.UseInMemoryDatabase(nameof(UserProfileService)));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddSwaggerGen(o =>
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UserProfile Service API",
                    Description = "UserProfile Service API Description"
                }));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserProfile Service API"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
