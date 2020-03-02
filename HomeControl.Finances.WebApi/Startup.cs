using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeControl.AccessControl.WebApi.Infrastructure.Settings;
using HomeControl.Finances.Infrastructure.Persistence.AccountData;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.WebApi.Infrastructure.MapperProfiles;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HomeControl.Finances.WebApi
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
            _configureCors(services);
            _configureThirdParty(services, mvcBuilder);
            _configureApplication(services);
        }

        private void _configureCors(IServiceCollection services)
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

        private void _configureApplication(IServiceCollection services)
        {
            //Infrastructure
            services.AddDbContext<AccountDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("HomeControl"));
                });
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
            services.AddTransient<IAccountTransferRepository, AccountTransferRepository>();
        }

        private void _configureThirdParty(IServiceCollection services, IMvcBuilder builder)
        {
            //Jwt
            services.AddTransient<IJwtHandler, JwtHandler>();
            services.Configure<IJwtConfiguration>(Configuration.GetSection("JwtConfiguration"));

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Fluent validations
            builder.AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddTransient<AbstractValidator<AccountRequest>, AccountValidator>();
            services.AddTransient<AbstractValidator<AccountTransactionRequest>, AccountTransactionValidator>();
            services.AddTransient<AbstractValidator<AccountTransferRequest>, AccountTransferValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
