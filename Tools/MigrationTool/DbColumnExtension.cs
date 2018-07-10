using System;
using System.Collections.Generic;
using System.Text;
using DbfReader;

namespace MigrationTool
{
    public static class DbColumnExtension
    {
        public static decimal ForceDecimal(this IDbfColumn column)
        {
            return string.IsNullOrWhiteSpace(column.ForceString())
                ? 0
                : column.GetDecimal();
        }
    }
}
