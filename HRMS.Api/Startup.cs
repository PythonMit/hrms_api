using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using HRMS.Api.Utility.Localization;
using HRMS.Services.Configuration;
using HRMS.Services.Mapper;
using HRMS.Core.Settings;
using HRMS.Core.Configuration;
using HRMS.DBL.Configuration;
using HRMS.Core.Consts;
using HRMS.Resources.Configuration;
using HRMS.Core.Exstensions;
using HRMS.Core.Utilities.Cipher;

namespace HRMS.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration _configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        public IWebHostEnvironment _environment { get; }

        private ApiSettings ApiSettings => _configuration.GetSection("ApiSettings").Get<ApiSettings>();
        private AppSettings AppSettings => _configuration.GetSection("AppSettings").Get<AppSettings>();

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureHRMSServices(services);
            ConfigureAuthentication(services);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthOptions.AuthPolicy, policy =>
                {
                    policy.RequireRole(RoleTypes.SuperAdmin.GetEnumDescriptionAttribute());
                    policy.RequireRole(RoleTypes.Admin.GetEnumDescriptionAttribute());
                    policy.RequireRole(RoleTypes.Employee.GetEnumDescriptionAttribute());
                    policy.RequireRole(RoleTypes.HRManager.GetEnumDescriptionAttribute());
                    policy.RequireRole(RoleTypes.Manager.GetEnumDescriptionAttribute());
                });
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddMemoryCache();

            services.AddLocalization(opt => opt.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                /* your configurations*/
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                };

                opts.DefaultRequestCulture = new RequestCulture("en", "en");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
                opts.AddInitialRequestCultureProvider(new AccessTokenRequestCultureProvider());
            });
        }

        private void ConfigureHRMSServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddLogging();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.ConfigureSettings(_configuration);
            services.ConfigureStores();
            services.ConfigureUtilities();
            services.ConfigureServices();
            services.ConfigureMapper();
            services.ConfigureResources();
            services.ConfigureDbContext(_configuration, _environment, AppSettings);            
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidateAudience = false,
                    ValidAudience = ApiSettings.AppUrl,
                    ValidateLifetime = true,
                    IssuerSigningKey = CipherUtils.GetSymmetricSecurityKey(AuthOptions.SecurityKey),
                    ValidateIssuerSigningKey = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = (context) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = (context) =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
