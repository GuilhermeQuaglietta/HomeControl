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
using HomeControl.AccessControl.WebApi.Requests.Recovery;
using HomeControl.AccessControl.WebApi.Requests.Users;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeControl.AccessControl.WebApi
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
            _configureMvc(services);
            _configureConnections(services);
            _configureUtilities(services);
            _configureDependencyInjection(services);
        }

        private void _configureMvc(IServiceCollection services)
        {
            //CORS
            CorsSettings corsSettings = Configuration.GetSection(CorsSettings.OptionsName).Get<CorsSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsSettings.PolicyName, builder =>
                {
                    builder.WithOrigins(corsSettings.AllowedOrigins);
                    builder.WithMethods(corsSettings.AllowedMethods);
                    builder.WithHeaders(corsSettings.AllowedHeaders);
                    builder.AllowCredentials();
                });
            });

            var mvcBuilder = services.AddMvc();
            _configureFluentValidations(services, mvcBuilder);

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(CorsSettings.PolicyName));
            });
        }

        private void _configureFluentValidations(IServiceCollection services, IMvcBuilder builder)
        {
            builder.AddFluentValidation(options =>
             {
                 options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
             });

            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<UserPostRequest>, UserPostRequestValidator>();
            services.AddTransient<IValidator<UserPutRequest>, UserPutRequestValidator>();
            services.AddTransient<IValidator<RecoveryPasswordChangeRequest>, RecoveryPasswordChangeRequestValidator>();
        }
        private void _configureConnections(IServiceCollection services)
        {
            services.AddDbContext<AccessControlDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("HomeControl"));
                });
        }

        private void _configureDependencyInjection(IServiceCollection services)
        {
            //Jwt
            services.AddTransient<IJwtHandler, JwtSymmetricHandler>();
            services.Configure<JwtConfiguration>(Configuration.GetSection("JwtConfiguration"));
            services.Configure<LoginSettings>(Configuration.GetSection("LoginSettings"));


            //Infrastructure
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserQueries, UserQueries>();

            //WebApi - Validators
            services.AddTransient<IValidator<UserPutRequest>, UserPutRequestValidator>();
            services.AddTransient<IValidator<UserPostRequest>, UserPostRequestValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
        }

        private void _configureUtilities(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsSettings.PolicyName);
            app.UseMvc();
        }
    }
}
