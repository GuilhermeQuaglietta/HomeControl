﻿using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeControl.AccessControl.WebApi.Infrastructure.Settings;
using HomeControl.Finances.Infrastructure.Persistence.AccountData;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
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
            services.AddDbContext<AccountDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("HomeControl"));
                });
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
            services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
            services.AddTransient<IAccountTransferRepository, AccountTransferRepository>();
        }

        private void ConfigureThirdParty(IServiceCollection services, IMvcBuilder builder)
        {
            //Jwt
            services.AddTransient<IJwtHandler, JwtHandler>();
            services.AddSingleton<IJwtConfiguration>(Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>());

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Fluent validations
            builder.AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddTransient<IValidator<AccountRequest>, AccountValidator>();
            services.AddTransient<IValidator<AccountTransactionRequest>, AccountTransactionValidator>();
            services.AddTransient<IValidator<AccountTransferRequest>, AccountTransferValidator>();
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
