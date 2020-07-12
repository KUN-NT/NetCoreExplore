using AutoMapper;
using JwtDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JwtDemo.Controllers
{
    /// <summary>
    /// Dto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DtoController : ControllerBase
    {
        private readonly IMapper _mapper;
        public DtoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<dynamic>> Get()
        {
            #region 模拟数据
            var allGoods = new List<GoodsEntity>() {
                new GoodsEntity()
                {
                    Name="Tom",
                    Author="Kun",
                    Content="test",
                    CreateTime=DateTime.Now,
                    IsDelete=true,
                    Price=10,
                    Title="Dto"
                },
                new GoodsEntity()
                {
                    Name="Jack",
                    Author="Kun2",
                    Content="test2",
                    IsDelete=false,
                    Price=10,
                    Title="Dto"
                },
                new GoodsEntity()
                {
                    Name="Rose",
                    Author="Kun3",
                    Content="test3",
                    CreateTime=DateTime.Now,
                    IsDelete=false,
                    Price=30,
                    Title="Dto"
                },
                new GoodsEntity()
                {
                    Name="Tom",
                    Author="Kun",
                    Content="test",
                    CreateTime=DateTime.Now,
                    IsDelete=true,
                    Price=50,
                    Title="Dto"
                },
                new GoodsEntity()
                {
                    Name="Tom",
                    Author="Kun",
                    Content="test",
                    CreateTime=DateTime.Now,
                    IsDelete=false,
                    Price=20,
                    Title="Dto"
                }
            };
            #endregion

            var goodsDto = _mapper.Map<List<GoodsDto>>(allGoods);
            //var goodsDto2 = (allGoods.AsQueryable()).ProjectTo<GoodsDto>(_mapper.ConfigurationProvider);

            return new List<dynamic>
            {
                goodsDto,
                //goodsDto2
            };
        }
    }
}
