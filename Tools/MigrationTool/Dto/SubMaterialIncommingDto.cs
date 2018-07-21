using System;

namespace MigrationTool.Dto
{
    public class SubMaterialIncommingDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int? Code { get; set; }

        public static SubMaterialIncommingDto CreateSubMaterialIncommingDtoFromMaterialDbModel(
            MaterialDbModel materialDbModel)
        {
            var codes = materialDbModel.Code.Split('-');
            int codeAsInt = Int32.Parse(codes[1]);

            SubMaterialIncommingDto subMaterialIncommingDto = new SubMaterialIncommingDto()
            {
                Name = materialDbModel.Name,
                Code = codeAsInt,
            };

            return subMaterialIncommingDto;
        }
    }
}
