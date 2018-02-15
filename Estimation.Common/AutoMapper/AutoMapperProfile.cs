using AutoMapper;
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
            CreateMap<MaterialIncommingDto, Material>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code.ToString()));

            CreateMap<MainMaterialIncommingDto, MaterialInfo>();
        }
    }
}
