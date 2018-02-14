using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess
{
    /// <summary>
    /// Material database context class
    /// </summary>
    public class MaterialDbContext: AppDbContext
    {
        /// <summary>
        /// Main materials database set
        /// </summary>
        public DbSet<MainMaterialDb> MainMaterials { get; set; }

        /// <summary>
        /// Material detail database set
        /// </summary>
        public DbSet<MaterialDb> Materials { get; set; }

        /// <summary>
        /// Material database context
        /// </summary>
        /// <param name="connectionString"></param>
        public MaterialDbContext(string connectionString):base(connectionString)
        {
        }
    }
}
