using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapMaterials();
        }

        private void MapMaterials()
        {
            CreateMap<MaterialIncommingDto, Material>();
                //.ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code.ToString()));

            CreateMap<MainMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, MainMaterialDb>();
            CreateMap<MainMaterialDb, MaterialInfo>();

            CreateMap<MainMaterial, MainMaterialDb>();
            CreateMap<MainMaterialDb, MainMaterial>();

            CreateMap<Material, MaterialDb>();
            CreateMap<MaterialDb, Material>();
        }
    }
}
