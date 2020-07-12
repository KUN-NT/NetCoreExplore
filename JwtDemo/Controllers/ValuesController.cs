using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IService _service;
        public ValuesController(IHttpContextAccessor accessor,IService service)
        {
            _accessor = accessor;
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _service.Say();
            _service.Say();
            //有效的令牌
            //1、有效的加密算法
            //2、数字签名->密钥

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,"kun"),
                new Claim(JwtRegisteredClaimNames.Email,"kun@qq.com"),
                new Claim(JwtRegisteredClaimNames.Sub,"1")
            };

            //创建密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcorejwttestkun"));

            //实例化token对象
            var token_simple = new JwtSecurityToken(
                claims: claims
                );

            var token2 = new JwtSecurityToken(
                "http://localhost:5000", "http://localhost:5000", claims, null, DateTime.Now.AddHours(1), new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityToken(
                issuer:"http://localhost:5000",
                audience: "http://localhost:5000",
                claims:claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            //生成token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new string[] { jwtToken };
        }

        // GET api/values/5
        [HttpGet("{jwtStr}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(string jwtStr)
        {
            //获取token内容的方法
            //1
            //var jwtHandler = new JwtSecurityTokenHandler();
            //JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

            //2
            var sub = User.FindFirst(d => d.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            //3
            var name = _accessor.HttpContext.User.Identity.Name;
            var claims = _accessor.HttpContext.User.Claims;
            var claimTypeVal = (from item in claims
                                where item.Type == JwtRegisteredClaimNames.Email
                                select item.Value).ToList();

            //return new string[] { JsonConvert.SerializeObject(jwtToken),name, JsonConvert.SerializeObject(claimTypeVal) };
            return new string[] { name, JsonConvert.SerializeObject(claimTypeVal) };
        }

    }
}
