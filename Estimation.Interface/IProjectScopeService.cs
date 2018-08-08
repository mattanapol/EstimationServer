using System.Collections.Generic;
using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Interface
{
    public interface IProjectScopeService
    {
        /// <summary>
        /// Gets the type of the available material.
        /// </summary>
        /// <returns></returns>
        IList<string> GetAvailableMaterialType();
        /// <summary>
        /// Gets the project scope template.
        /// </summary>
        /// <param name="materialType">Type of the material.</param>
        /// <returns></returns>
        ProjectScopeOfWorkGroup GetProjectScopeTemplate(string materialType);
        /// <summary>
        /// Gets the project scope of work report.
        /// </summary>
        /// <param name="projectScopeOfWorkGroup">The project scope of work group.</param>
        /// <returns></returns>
        Task<byte[]> GetProjectScopeOfWorkReport(ProjectScopeOfWorkGroup projectScopeOfWorkGroup);
    }
}