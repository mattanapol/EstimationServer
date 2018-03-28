using Estimation.Domain.Models;
using Estimation.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ProjectSummaryService : IProjectSummaryService
    {
        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ProjectSummary> GetGroupSummary(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get project summary by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ProjectSummary> GetProjectSummary(int id)
        {
            throw new NotImplementedException();
        }
    }
}
