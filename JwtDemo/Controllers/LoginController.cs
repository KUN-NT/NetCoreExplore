using JwtDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwtDemo.Controllers
{
    /// <summary>
    /// 跨域
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public void TestJsonpForVue(string callback)
        {
            GoodsDto goods = new GoodsDto()
            {
                GoodsName = "JSONP",
                Author = "Kun",
                Price = 10
            };
            string call = callback + "(" + JsonConvert.SerializeObject(goods) + ")";
            Response.WriteAsync(call);
        }

        [HttpPost]
        public ActionResult TestProxyForVue()
        {
            GoodsDto goods = new GoodsDto()
            {
                GoodsName = "Proxy",
                Author = "Kun",
                Price = 10
            };
            return Ok(goods);
        }

        [HttpPut]
        public ActionResult TestCorsForVue()
        {
            GoodsDto goods = new GoodsDto()
            {
                GoodsName = "Cors",
                Author = "Kun",
                Price = 10
            };
            return Ok(goods);
        }
    }
}
