using AutoMapper;
using Estimation.DataAccess.Models;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Kaewsai.Utilities.Configurations.Models;
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

            CreateMap<MaterialInfo, MaterialDb>();
            CreateMap<MaterialDb, MaterialInfo>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.SubMaterial != null && src.SubMaterial.MainMaterial != null)
                        dest.Class = src.SubMaterial.MainMaterial.Class;
                });

            CreateMap<Material, MaterialDb>();
            CreateMap<MaterialDb, Material>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.SubMaterial.MainMaterial.Code.ToString()}-{src.SubMaterial.Code.ToString("D2")}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.SubMaterial != null && src.SubMaterial.MainMaterial != null)
                        dest.Class = src.SubMaterial.MainMaterial.Class;
                });
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
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.MainMaterial != null)
                        dest.Class = src.MainMaterial.Class;
                });

            CreateMap<SubMaterial, SubMaterialDb>();
            CreateMap<SubMaterialDb, SubMaterial>()
                .ForMember(dest => dest.CodeAsString, opts => opts.MapFrom(src => $"{src.MainMaterial.Code.ToString()}-{src.Code.ToString("D2")}"))
                .AfterMap((src, dest) => {
                    if (src.MainMaterial != null)
                        dest.Class = src.MainMaterial.Class;
                });
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
                .ForMember(dest => dest.MiscellaneousManual, opts => opts.MapFrom(src => src.Miscellaneous.Manual))
                .ForMember(dest => dest.MiscellaneousPercentage, opts => opts.MapFrom(src => src.Miscellaneous.Percentage))
                .ForMember(dest => dest.MiscellaneousIsUsePercentage, opts => opts.MapFrom(src => src.Miscellaneous.IsUsePercentage))
                .ForMember(dest => dest.TransportationManual, opts => opts.MapFrom(src => src.Transportation.Manual))
                .ForMember(dest => dest.TransportationPercentage, opts => opts.MapFrom(src => src.Transportation.Percentage))
                .ForMember(dest => dest.TransportationIsUsePercentage, opts => opts.MapFrom(src => src.Transportation.IsUsePercentage))
                .ForMember(dest => dest.Materials, opts => opts.MapFrom(src => Mapper.Map<IEnumerable<ProjectMaterial>, List<ProjectMaterialDb>>(src.Materials)));
            CreateMap<MaterialGroupDb, ProjectMaterialGroup>()
                .ForMember(dest => dest.Miscellaneous, opts => opts.MapFrom(src => new Cost() { Percentage = src.MiscellaneousPercentage, Manual = src.MiscellaneousManual, IsUsePercentage = src.MiscellaneousIsUsePercentage }))
                .ForMember(dest => dest.Transportation, opts => opts.MapFrom(src => new Cost() { Percentage = src.TransportationPercentage, Manual = src.TransportationManual, IsUsePercentage = src.TransportationIsUsePercentage }));
        }

        private void MapProjectMaterial()
        {
            CreateMap<ProjectMaterialDb, ProjectMaterial>();
            CreateMap<ProjectMaterial, ProjectMaterialDb>();

            CreateMap<ProjectMaterialIncomingDto, ProjectMaterial>();
        }

        private void MapConfiguration()
        {
            CreateMap<ConfigurationDict, ConfigurationDictDto>();
            CreateMap<ConfigurationDictDto, ConfigurationDict>();
        }
    }
}
