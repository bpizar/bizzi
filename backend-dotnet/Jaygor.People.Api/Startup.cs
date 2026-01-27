using System;
using System.Net;
using System.Text;
using JayGor.People.Api.auth;
using JayGor.People.DataAccess;
using JayGor.People.DataAccess.Factories.MySqlServer;
using JayGor.People.DataAccess.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Jaygor.People.Api
{
    public class Startup
  {
        //static readonly BussinnessLayer negocio = new BussinnessLayer();

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
            //services.AddSingleton<IJwtFactory, JwtFactory>();

            var connectionString = Configuration.GetConnectionString(string.Format("EntityFramework.MySql_ConnectionString"));
            services.AddSwaggerGen();
            // services.AddDbContextPool<MySqlContextDB>(options => { options.UseMySql(connectionString); } );


            services.AddControllers();
            // other service configurations go here
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 19));
            services.AddDbContextPool<MySqlContextDB>( // replace "YourDbContext" with the class name of your DbContext
                options => options.UseMySql(connectionString, // replace with your Connection String
                    serverVersion
            ));


            //  services.AddTransient<AddDbContextPool, MySqlContextDB>(options => options.UseMySql(connectionString));

            //services.AddScoped<IDatabaseService, MySqlDatabaseService>();

            services.AddSingleton<IJwtFactory, JwtFactory>();


            services.AddScoped<IDatabaseService, MySqlDatabaseService>();
            // services.AddHostedService<ChatDemon>();
            // services.AddHostedService<NotificationDemon>();


            //services <ConsumeScopedServiceHostedService>();

            //            services.AddSingleton<IHostedService, ChatDemon>();
            //services.AddSingleton<IHostedService, ChatDemon>();





            //services.AddSingleton<IHostedService, NotificationDemon>();

            //services.add <IHostedService, ChatDemon>();
            // services.AddTransient<IHostedService, NotificationDemon>();


            //services.AddSingleton<IJwtFactory, JwtFactory>();
            //services.AddSingleton<IHostedService, ChatDemon>();
            //services.AddSingleton<IHostedService,NotificationDemon>();

            //services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

           

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            string SecretKey = jwtAppSettingOptions[nameof(JwtIssuerOptions.SigningKey)]; // "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
			SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

			services.Configure<JwtIssuerOptions>(options =>
			{
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
           
			});


            // nuevo
			services.Configure<IISOptions>(options =>
			{
				options.ForwardClientCertificate = false;
			});

            // services.Configure<RequestLocalizationOptions>(options=>
            // {
            //     options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-Es");
            // });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,                 
			};

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(configureOptions =>
			{
				configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				configureOptions.TokenValidationParameters = tokenValidationParameters;
				configureOptions.SaveToken = true;
			});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireRole(Constants.Strings.JwtClaims.ApiAccess));// .RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
			});

            // var connectionString = Configuration.GetConnectionString(string.Format("EntityFramework.MySql_ConnectionString"));
            // services.AddDbContextPool<MySqlContextDB>(options => options.UseMySql(connectionString));
            // services.AddScoped<IDatabaseService, MySqlDatabaseService>();

            // services.AddSingleton<IJwtFactory, JwtFactory>();
            // services.AddSingleton<IHostedService, ChatDemon>();
            // services.AddSingleton<IHostedService, NotificationDemon>();
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            // 
            app.UseRequestLocalization();

			app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(
				builder =>
				{
					builder.Run(
						async context =>
							{
								context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
								context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

								var error = context.Features.Get<IExceptionHandlerFeature>();
								if (error != null)
								{
									context.Response.AddApplicationError(error.Error.Message);
									await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
								}
							});
				});

			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


            app.UseAuthentication();



			app.UseDefaultFiles();
			app.UseStaticFiles();
            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseAuthorization();
            app.UseEndpoints(EndPoints =>
            {
                EndPoints.MapControllers();
            }
            );
           
		}
    }
}