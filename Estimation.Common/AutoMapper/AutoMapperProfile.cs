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
            MapMainMaterials();
            MapSubMaterials();
            MapMaterials();
            MapProjectInfos();
            MapMaterialGroups();
            MapProjectMaterial();
        }

        private void MapMaterials()
        {
            CreateMap<MaterialIncommingDto, Material>();
            //.ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code.ToString()));

            CreateMap<MaterialInfo, MaterialDb>();
            CreateMap<MaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"));

            CreateMap<Material, MaterialDb>();
            CreateMap<MaterialDb, Material>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"));
        }

        private void MapMainMaterials()
        {
            CreateMap<MainMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, MainMaterialDb>();
            CreateMap<MainMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"));

            CreateMap<MainMaterial, MainMaterialDb>();
            CreateMap<MainMaterialDb, MainMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.Code.ToString()}"));
        }

        private void MapSubMaterials()
        {
            CreateMap<SubMaterialIncommingDto, MaterialInfo>();

            CreateMap<MaterialInfo, SubMaterialDb>();
            CreateMap<SubMaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"));

            CreateMap<SubMaterial, SubMaterialDb>();
            CreateMap<SubMaterialDb, SubMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"));
        }

        private void MapProjectInfos()
        {
            CreateMap<ProjectInfoIncommingDto, ProjectInfo>()
                .ForMember(dest => dest.CreatedDate, opts => opts.Ignore())
                .ForMember(dest => dest.LastModifiedDate, opts => opts.Ignore());
            CreateMap<ProjectInfo, ProjectInfoLightDto>();

            CreateMap<ProjectInfo, ProjectInfoDb>()
                .ForMember(dest => dest.MiscellaneousManual, opts => opts.MapFrom(src => src.Miscellaneous.Manual))
                .ForMember(dest => dest.MiscellaneousPercentage, opts => opts.MapFrom(src => src.Miscellaneous.Percentage))
                .ForMember(dest => dest.MiscellaneousIsUsePercentage, opts => opts.MapFrom(src => src.Miscellaneous.IsUsePercentage))
                .ForMember(dest => dest.TransportationManual, opts => opts.MapFrom(src => src.Transportation.Manual))
                .ForMember(dest => dest.TransportationPercentage, opts => opts.MapFrom(src => src.Transportation.Percentage))
                .ForMember(dest => dest.TransportationIsUsePercentage, opts => opts.MapFrom(src => src.Transportation.IsUsePercentage));
            CreateMap<ProjectInfoDb, ProjectInfo>()
                .ForMember(dest => dest.Miscellaneous, opts => opts.MapFrom(src => new Cost() { Percentage = src.MiscellaneousPercentage, Manual = src.MiscellaneousManual, IsUsePercentage = src.MiscellaneousIsUsePercentage }))
                .ForMember(dest => dest.Transportation, opts => opts.MapFrom(src => new Cost() { Percentage = src.TransportationPercentage, Manual = src.TransportationManual, IsUsePercentage = src.TransportationIsUsePercentage }));
        }

        private void MapMaterialGroups()
        {
            CreateMap<ProjectMaterialGroupIncomingDto, ProjectMaterialGroup>();
            CreateMap<ProjectMaterialGroupUpdateIncomingDto, ProjectMaterialGroup>();

            CreateMap<ProjectMaterialGroup, MaterialGroupDb>()
                .ForMember(dest => dest.Materials, opts => opts.MapFrom(src => Mapper.Map<IEnumerable<Material>, List<ProjectMaterialDb>>(src.Materials)));
            CreateMap<MaterialGroupDb, ProjectMaterialGroup>();
        }

        private void MapProjectMaterial()
        {
            CreateMap<ProjectMaterialDb, Material>();
            CreateMap<Material, ProjectMaterialDb>();

            CreateMap<ProjectMaterialIncomingDto, Material>();
        }
    }
}
