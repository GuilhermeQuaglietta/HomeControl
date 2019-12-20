using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeControl.Finances.Infrastructure.Persistence.AccountData;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.WebApi.Infrastructure.MapperProfiles;
using HomeControl.Finances.WebApi.v1.Message.AccountMessage;
using HomeControl.Identity.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeControl.Finances.WebApi
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void _configureMvc(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin());
                opt.AddPolicy("AllowAnyMethod", builder => builder.AllowAnyHeader());
                opt.AddPolicy("AllowAnyHeader", builder => builder.AllowAnyMethod());
            });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(options =>
                {
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
        }

        private void _configureConnections(IServiceCollection services)
        {
            services.AddDbContext<AccountDbContext>(
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

            //Infrastructure
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
            services.AddTransient<IAccountTransferRepository, AccountTransferRepository>();

            //WebApi - Validators
            _configureValidators(services);
        }

        private void _configureUtilities(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
        }

        private void _configureValidators(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<AccountRequest>, AccountValidator>();
            services.AddTransient<AbstractValidator<AccountTransactionRequest>, AccountTransactionValidator>();
            services.AddTransient<AbstractValidator<AccountTransferRequest>, AccountTransferValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAnyOrigin");
            app.UseCors("AllowAnyMethod");
            app.UseCors("AllowAnyHeader");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
