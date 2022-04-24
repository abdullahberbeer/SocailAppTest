using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerAppTwo.Data;

using ServerAppTwo.Models;


namespace ServerAppTwo
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
            services.AddDbContext<SocialAppContext>(x=>x.UseSqlite("Data Source=socialtwo.db"));
            services.AddIdentity<User,Role>().AddEntityFrameworkStores<SocialAppContext>();
            services.Configure<IdentityOptions>(options=>{
                options.Password.RequireDigit=false;
                options.Password.RequireUppercase=false;
                 options.Password.RequireNonAlphanumeric=false;
                 options.Password.RequiredLength=6;

                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts=5;
                options.Lockout.AllowedForNewUsers=true;

                options.User.RequireUniqueEmail=true;
            });    
            services.AddControllers().AddNewtonsoftJson(optinos=>{
                optinos.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors(options=>{
                options.AddPolicy(
                    name:"MyAllowOrigns",
                    builder=>{
                        builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });
            services.AddAuthentication(x=>{
                    x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=>{
                x.RequireHttpsMetadata=false;
                x.SaveToken=true;
                x.TokenValidationParameters=new TokenValidationParameters{
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerAppTwo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                 SeedDatabase.Seed(userManager).Wait();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerAppTwo v1"));
            }else{
                // app.UseExceptionHandler(appError=>{
                //     appError.Run(async context=>{
                //       context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                //       context.Response.ContentType="application/json";
                //       var exception=context.Features.Get<IExceptionHandlerFeature>();
                //       if(exception!=null){
                //           await context.Response.WriteAsync(new ErrorDetails(){
                //                 StatusCode=context.Response.StatusCode,
                //                 Message=exception.Error.Message
                //           }.ToString());
                //       }      
                //     });
                // });
                app.UseExceptionHandler(appError=>{
                    appError.Run(async context=>{
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType="application/json";
                    var exception=context.Features.Get<IExceptionHandlerFeature>();
                    if(exception!=null){
                        await context.Response.WriteAsync(new ErrorDetails(){
                            StatusCode=context.Response.StatusCode,
                            Message=exception.Error.Message
                        }.ToString());
                    }
                    });
                });
                 app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerAppTwo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
app.UseCors("MyAllowOrigns");
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
