using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Options;
using ixiaBackend_application.Services;
using ixiaBackend_application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ixiaBackend_application
{
    public partial class Startup
    {
        private readonly IConfiguration config;
        private readonly Security securityOptions;
        private readonly Swagger swaggerOptions;

        public Startup(IConfiguration config)
        {
            this.config = config;
            securityOptions = config.GetSection(nameof(Security)).Get<Security>();
            swaggerOptions = config.GetSection(nameof(Swagger)).Get<Swagger>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Security>(config.GetSection(nameof(Security)));
            services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));

            services.AddControllers()
                .AddNewtonsoftJson(JsonOptions);

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<IxiaContext>()
               .AddDefaultTokenProviders();

            services.AddDbContext<IxiaContext>
                (options => options.UseSqlServer(config.GetConnectionString("Ixia")));

            services.AddAutoMapper(typeof(Startup));


            services.AddAuthentication(AuthenitcationOptions)
                .AddJwtBearer(JwtOptions);
            //   .AddGoogle(googleOptions =>
            //   {
            //       googleOptions.ClientId = "717521932893-jajh3aifsgjni4kvigtjaqoht2m7ucmc.apps.googleusercontent.com";
            //       googleOptions.ClientSecret = "VAj-qr_NXnsrkcrqQNrtOawD";
            //   }); 

            services.AddMvc();

            services.AddSwaggerGen(SwaggerOptions);

            services.AddCors();
            services.AddHttpClient();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPriceService, PriceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.RouteTemplate);
            app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UiEndpoint, nameof(ixiaBackend_application)));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("api", "api/{controller}/{action}/{id?}");
            });
        }
    }
}
