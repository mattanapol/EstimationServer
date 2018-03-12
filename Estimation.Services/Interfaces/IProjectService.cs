using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Get project information by project id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<ProjectInfo> GetProject(int projectId);
    }
}
