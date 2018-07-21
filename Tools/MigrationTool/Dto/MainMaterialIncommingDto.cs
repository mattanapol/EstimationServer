using System;
using MigrationTool.DbModel;

namespace MigrationTool.Dto
{
    /// <summary>
    /// Main material incomming dto
    /// </summary>
    public class MainMaterialIncommingDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        public MaterialClass Class { get; set; }

        public static MainMaterialIncommingDto CreateMainMaterialIncommingDtoFromMaterialDbModel(
            MaterialDbModel materialDbModel, string materialType)
        {
            var codes = materialDbModel.Code.Split('-');
            int codeAsInt = Int32.Parse(codes[0].Substring(1));

            MainMaterialIncommingDto mainMaterialIncommingDto = new MainMaterialIncommingDto()
            {
                Name = materialDbModel.Name,
                Code = codeAsInt,
                MaterialType = materialType,
                Class = MaterialClass.MainEquipment
            };

            return mainMaterialIncommingDto;
        }
    }
}
