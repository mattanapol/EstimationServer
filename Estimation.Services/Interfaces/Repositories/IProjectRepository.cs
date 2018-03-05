using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectInfo>> GetAllProjectInfo();

        Task<ProjectInfo> CreateProjectInfo(ProjectInfo projectInfo);

        Task<ProjectInfo> GetProjectInfo(int id);
    }
}
