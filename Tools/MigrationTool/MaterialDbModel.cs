using System;
using System.Collections.Generic;
using System.Text;
using DbfReader;

namespace MigrationTool
{
    public class MaterialDbModel
    {
        public string Code { get; set; }

        public string Name { get; set; }
        
        public bool HasSub { get; set; }

        public int NoOfSub { get; set; }

        public string DatabaseFileName { get; set; }

        public string MaterialClass { get; set; }

        public static MaterialDbModel CreateMaterialDbModelFromRow(IDbfRow row)
        {
            MaterialDbModel materialDbModel = new MaterialDbModel()
            {
                Code = row["CODE"]
                    .GetString().Trim(),
                Name = row["MATERIAL"]
                    .GetString()
                    .Trim(),
                HasSub = row["HAS_SUB"]
                    .ForceString() == "T",
                DatabaseFileName = row["MFILENAME"]
                    .GetString().Trim(),
                NoOfSub = row["NO_OF_SUB"].GetInt(),
                MaterialClass = row["MCLASS"]
                    .GetString().Trim(),
            };


            return materialDbModel;
        }
    }
}
