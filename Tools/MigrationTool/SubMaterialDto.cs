using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationTool
{
    public class SubMaterialDto
    {
        public SubMaterialIncommingDto SubMaterialIncommingDto { get; set; }

        public string SubMaterialId { get; set; }

        public string DbFileName { get; set; }
    }
}
