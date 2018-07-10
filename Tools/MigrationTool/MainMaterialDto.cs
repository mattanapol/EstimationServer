using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationTool
{
    public class MainMaterialDto
    {
        public MainMaterialIncommingDto MainMaterialIncommingDto { get; set; }

        public List<SubMaterialDto> SubMaterialDtos { get; set; }

        public string MainMaterialId { get; set; }
    }
}
