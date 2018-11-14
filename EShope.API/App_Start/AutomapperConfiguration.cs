using AutoMapper;
using AutoMapper.Configuration;
using EShope.API.DataObjects;
using EShope.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShope.API.App_Start
{
    public class AutomapperConfiguration
    {
        public static void CreateMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductDTO>();

            cfg.CreateMap<ProductDTO, Product>();
        }
    }
}