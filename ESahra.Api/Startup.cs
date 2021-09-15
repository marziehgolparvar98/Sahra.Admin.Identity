using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sahara.Common;
using Sahra.Admin.Api.Configuration;
using Sahra.Admin.Api.Configuration.Authorization;
using Sahra.Admin.Api.ExceptionHandling;
using Sahra.Admin.Api.Helpers;
using Sahra.Admin.Api.Mappers;
using Sahra.Admin.Api.Resources;
using Sahra.Admin.EntityFramework.Shared.DbContexts;
using Sahra.Admin.EntityFramework.Shared.Entities.Identity;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Repository;
using Sahra.Services.IService;
using Sahra.Services.Service;
using Sahra.Shared.Dtos;
using Sahra.Shared.Dtos.Identity;
using Serilog;
using Skoruba.AuditLogging.Constants;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.AuditLogging.EntityFramework.Extensions;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using System;
using System.Collections.Generic;

namespace Sahra.Accelerator.api
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
            var adminApiConfiguration = Configuration.GetSection(nameof(AdminApiConfiguration)).Get<AdminApiConfiguration>();
            services.AddSingleton(adminApiConfiguration);

            // Add DbContexts
            RegisterDbContexts(services);

            services.AddDataProtection<IdentityServerDataProtectionDbContext>(Configuration);

            // Add email senders which is currently setup for SendGrid and SMTP
            services.AddEmailSenders(Configuration);

            services.AddScoped<ControllerExceptionFilterAttribute>();
            services.AddScoped<IApiErrorResources, ApiErrorResources>();

            // Add authentication services
            RegisterAuthentication(services);

            // Add authorization services
            RegisterAuthorization(services);

            var profileTypes = new HashSet<Type>
            {
                typeof(IdentityMapperProfile<IdentityRoleDto, IdentityUserRolesDto, string, IdentityUserClaimsDto, IdentityUserClaimDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto, IdentityRoleClaimDto, IdentityRoleClaimsDto>)
            };

            services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext,
                IdentityUserDto, IdentityRoleDto, UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
                IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
                IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
                IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(profileTypes);

            services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();

            services.AddAdminApiCors(adminApiConfiguration);

            services.AddMvcServices<IdentityUserDto, IdentityRoleDto,
                UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
                IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
                IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
                IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(adminApiConfiguration.ApiVersion, new OpenApiInfo { Title = adminApiConfiguration.ApiName, Version = adminApiConfiguration.ApiVersion });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token"),
                            Scopes = new Dictionary<string, string> {
                                { adminApiConfiguration.OidcApiName, adminApiConfiguration.ApiName }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddAuditEventLogging<AdminAuditLogDbContext, AuditLog>(Configuration);

            services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>(Configuration, adminApiConfiguration);

            services.AddControllers()
            .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );
            // For Entity Framework 
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("StartUpConnection"));

            });
            services.AddRateLimit(Configuration);
            services.AddAuditLogging(options =>
            {
                options.UseDefaultSubject = true;
                options.UseDefaultAction = true;
            })
                  .AddDefaultHttpEventData(subjectOptions =>
                  {
                      subjectOptions.SubjectIdentifierClaim = ClaimsConsts.Sub;
                      subjectOptions.SubjectNameClaim = ClaimsConsts.Name;
                  },
                      actionOptions =>
                      {
                          actionOptions.IncludeFormVariables = true;
                      })
                  .AddDefaultStore(options => options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")))
                  .AddDefaultAuditSink();
            services.AddDbContext<ESahraApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("ESahraConnection"));

            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IResumeRepository, ResumeRepository>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddScoped<IUploadFileRepository, UploadFileRepository>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<ISolutionRepository, SolutionRepository>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddSingleton<Serilog.ILogger>(Log.Logger);

            //services.AddSwaggerGen(options =>
            //{
            //    //options.SwaggerDoc(adminApiConfiguration.ApiVersion, new OpenApiInfo { Title = adminApiConfiguration.ApiName, Version = adminApiConfiguration.ApiVersion });

            //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.OAuth2,
            //        Flows = new OpenApiOAuthFlows
            //        {
            //            AuthorizationCode = new OpenApiOAuthFlow
            //            {
            //                AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
            //                TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token"),
            //                Scopes = new Dictionary<string, string> {
            //                    { adminApiConfiguration.OidcApiName, adminApiConfiguration.ApiName }
            //                }
            //            }
            //        }
            //    });
            //    options.OperationFilter<AuthorizeCheckOperationFilter>();
            //});
            services.AddSwaggerGen(current =>
            {
                current.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                current.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            Scheme = "oauth2",

                            In =
                             ParameterLocation.Header,

                            Reference =
                                new OpenApiReference
                                {
                                    Type =ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AdminApiConfiguration adminApiConfiguration)
        {
            app.AddForwardHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESahra.Api v1");
                c.RoutePrefix = "";
                c.OAuthClientId(adminApiConfiguration.OidcSwaggerUIClientId);
                c.OAuthAppName(adminApiConfiguration.ApiName);
                c.OAuthUsePkce();
            });


            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


            app.UseHttpsRedirection();

            //  app.SeedData();

            app.UseRouting();

            app.UseIpRateLimiting();

            UseAuthentication(app);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        public virtual void RegisterDbContexts(IServiceCollection services)
        {
            services.AddDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext, AuditLog>(Configuration);
        }

        public virtual void RegisterAuthentication(IServiceCollection services)
        {
            services.AddApiAuthentication<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
        }

        public virtual void RegisterAuthorization(IServiceCollection services)
        {
            services.AddAuthorizationPolicies();
        }

        public virtual void UseAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}




