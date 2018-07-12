using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Estimation.Domain.Dtos;
using Kaewsai.Utilities.Configurations.Models;

namespace Estimation.Common.AutoMapper.Profiles
{
    /// <summary>
    /// Configuration profile class
    /// </summary>
    /// <seealso cref="Profile" />
    public class ConfigurationProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationProfile"/> class.
        /// </summary>
        public ConfigurationProfile()
        {
            CreateMap<ConfigurationDict, ConfigurationDictDto>().ReverseMap();

            CreateMap<ConfigurationEntryDto, ConfigurationEntry>().ReverseMap();
        }
    }
}
