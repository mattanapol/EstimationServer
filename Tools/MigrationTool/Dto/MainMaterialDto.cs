using System.Collections.Generic;

namespace MigrationTool.Dto
{
    public class MainMaterialDto
    {
        public MainMaterialIncommingDto MainMaterialIncommingDto { get; set; }

        public List<SubMaterialDto> SubMaterialDtos { get; set; }

        public string MainMaterialId { get; set; }
    }
}
