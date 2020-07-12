using AutoMapper;
using JwtDemo.Models;
using System;

namespace JwtDemo.Configs
{
    public class GoodsProfiles:Profile
    {
        //添加实体映射
        public GoodsProfiles()
        {
            //GoodsEntity转GoodsDto
            CreateMap<GoodsEntity, GoodsDto>()
                //映射发生之前
                .BeforeMap((src, dest) => src.Author = src.Author.ToUpper())
                .BeforeMap((src, dest) => src.CreateTime = src.CreateTime == null ? (new DateTime(2020, 01, 01)) : src.CreateTime)

                //映射匹配
                .ForMember(dest => dest.GoodsName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price + 10))
                //忽略某个属性的映射
                .ForMember(dest => dest.IsDelete, opt => opt.Ignore())

                //映射发生之后
                .AfterMap((src, dest) => dest.GoodsName = dest.Price > 40 ? "N/A" : dest.GoodsName);

            //CreateMap<GoodsDto, GoodsEntity>();
        }
    }
}
