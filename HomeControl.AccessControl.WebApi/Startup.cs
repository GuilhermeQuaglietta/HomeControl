using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeControl.AccessControl.Domain.Seedwork;
using HomeControl.AccessControl.Domain.Users;
using HomeControl.AccessControl.Infrastructure.Queries;
using HomeControl.AccessControl.Infrastructure.Repository;
using HomeControl.AccessControl.Infrastructure.Seedwork;
using HomeControl.AccessControl.WebApi.Infrastructure.Settings;
using HomeControl.AccessControl.WebApi.Infrastructure.Validators;
using HomeControl.AccessControl.WebApi.Requests.Login;
using HomeControl.AccessControl.WebApi.Requests.Users;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HomeControl.AccessControl.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var mvcBuilder = services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            ConfigureCors(services);
            ConfigureThirdParty(services, mvcBuilder);
            ConfigureApplication(services);
        }

        private void ConfigureCors(IServiceCollection services)
        {
            CorsSettings corsSettings = Configuration.GetSection(CorsSettings.OptionsName).Get<CorsSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsSettings.PolicyName, builder =>
                {
                    builder.WithOrigins(corsSettings.AllowedOrigins);
                    builder.WithMethods(corsSettings.AllowedMethods);
                    builder.WithHeaders(corsSettings.AllowedHeaders);
                });
            });
        }

        private void ConfigureApplication(IServiceCollection services)
        {
            //Infrastructure
            services.AddDbContext<AccessControlDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("HomeControl"));
                });
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserQueries, UserQueries>();

            //Domain
            services.AddSingleton(Configuration.GetSection("LoginSettings").Get<LoginSettings>());
        }

        private void ConfigureThirdParty(IServiceCollection services, IMvcBuilder builder)
        {
            //Jwt
            services.AddTransient<IJwtHandler, JwtHandler>();
            services.AddSingleton<IJwtConfiguration>(Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>());

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Fluent validation
            builder.AddFluentValidation(options =>
             {
                 options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
             });

            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<UserPutRequest>, UserPutRequestValidator>();
            services.AddTransient<IValidator<UserPostRequest>, UserPostRequestValidator>();
            services.AddTransient<IValidator<RecoveryPasswordChangeRequest>, RecoveryPasswordChangeRequestValidator>();
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(CorsSettings.PolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc();
        }
    }
}
