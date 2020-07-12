using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IService, Service>();

            #region Jwt
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //清除JwtSecurityToken进行清除 禁止netcore进行配置映射
            //不写这句话 netcore会将jwt方式声明的令牌变量映射成netcore方式变量，导致用第三种方式获取token内容失败
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //JwtToken认证
            var key = "aspnetcorejwttestkun";
            var keyByteArray = Encoding.ASCII.GetBytes(key);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                //数字签名
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                //发行人
                ValidIssuer = "http://localhost:5000",
                ValidateAudience = true,
                //订阅人
                ValidAudience = "http://localhost:5000",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true
            };

            //开启bearer认证
            //注册jwt bearer服务
            services.AddAuthentication("Bearer").AddJwtBearer(p =>
            {
                p.TokenValidationParameters = tokenValidationParameters;
            });
            #endregion

            services.AddAutoMapper(typeof(Startup));

            #region Cors
            services.AddCors(p =>
                {
                    p.AddPolicy("LimitRequests", policy =>
                    {
                        policy
                        //指定运行跨域的域名集合
                        .WithOrigins("http://127.0.0.1:8080", "http://localhost:8080")
                        //任何请求头
                        .WithHeaders()
                        //任何请求方法
                        .AllowAnyMethod();
                    });
                }); 
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //添加Cors中间件
            app.UseCors("LimitRequests");

            //开启认证中间件
            //不开启无法将认证的令牌注册到Http上下文
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
