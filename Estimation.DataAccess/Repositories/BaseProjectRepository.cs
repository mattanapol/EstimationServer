using Estimation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Repositories
{
    public abstract class BaseProjectRepository
    {
        /// <summary>
        /// The database context with an open connection.
        /// </summary>
        protected ProjectDbContext DbContext { get; }

        /// <summary>
        /// Gets the type mapping service.
        /// </summary>
        /// <value>The type mapping service.</value>
        protected ITypeMappingService TypeMappingService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Estimation.DataAccess.Repositories.BaseProjectRepository"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        /// <param name="typeMappingService">Type mapping service.</param>
        protected BaseProjectRepository(ProjectDbContext dbContext, ITypeMappingService typeMappingService)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            TypeMappingService = typeMappingService ?? throw new ArgumentNullException(nameof(typeMappingService));
        }
    }
}
