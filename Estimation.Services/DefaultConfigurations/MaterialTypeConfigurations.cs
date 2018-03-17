using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Services.DefaultConfigurations
{
    public class MaterialTypeConfigurations: ConfigurationDict
    {
        public static readonly ConfigurationEntry ElectricType = new ConfigurationEntry() { Label = "Electric", Value = null, InternalDescription = "Electronic material type." };
        public static readonly ConfigurationEntry MechanicType = new ConfigurationEntry() { Label = "Machanic", Value = null, InternalDescription = "Machanic material type." };
        public static readonly ConfigurationEntry ComputerType = new ConfigurationEntry() { Label = "Computer", Value = null, InternalDescription = "Computer material type." };

        public static readonly string DictTitle = "Materail Type";
        public static readonly string DictDescription = "List of material types.";

        public MaterialTypeConfigurations()
        {
            Title = DictTitle;
            Description = DictDescription;
            ConfigurationEntries = new List<ConfigurationEntry>(new ConfigurationEntry[]{
                ElectricType,
                MechanicType,
                ComputerType
            });
        }
    }
}
