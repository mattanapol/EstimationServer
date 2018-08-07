using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Estimation.Domain.Models;
using Estimation.Interface;

namespace Estimation.Services
{
    public class ProjectScopeService : IProjectScopeService
    {
        private string ElectricMaterialTypeName => "Electrical";

        private string MechanicMaterialTypeName => "Mechanical";

        private string ElectricScopeOfWorkTemplatePath => @".\ProgramData\ScopeOfWork_Electrical.txt";

        private string MechanicScopeOfWorkTemplatePath => @".\ProgramData\ScopeOfWork_Mechanical.txt";

        public ProjectScopeService()
        {

        }

        /// <inheritdoc />
        public IList<string> GetAvailableMaterialType()
        {
            return new List<string>() { ElectricMaterialTypeName, MechanicMaterialTypeName };
        }

        /// <inheritdoc />
        public ProjectScopeOfWorkGroup GetProjectScopeTemplate(string materialType)
        {
            if (materialType == ElectricMaterialTypeName)
                return GetElectricProjectScopeTemplate();

            if (materialType == MechanicMaterialTypeName)
                return GetMechanicProjectScopeTemplate();

            return GetElectricProjectScopeTemplate();
        }

        /// <inheritdoc />
        public byte[] GetProjectScopeOfWorkReport(ProjectScopeOfWorkGroup projectScopeOfWorkGroup)
        {
            return null;
        }

        private ProjectScopeOfWorkGroup GetElectricProjectScopeTemplate()
        {
            var scopeOfWorkGroup = new ProjectScopeOfWorkGroup() {MaterialType = ElectricMaterialTypeName };
            scopeOfWorkGroup.ScopeOfWorks = GetScopeOfWorkListFromTemplateFile(ElectricScopeOfWorkTemplatePath);

            return scopeOfWorkGroup;
        }

        private ProjectScopeOfWorkGroup GetMechanicProjectScopeTemplate()
        {
            var scopeOfWorkGroup = new ProjectScopeOfWorkGroup() { MaterialType = MechanicMaterialTypeName };
            scopeOfWorkGroup.ScopeOfWorks = GetScopeOfWorkListFromTemplateFile(MechanicScopeOfWorkTemplatePath);

            return scopeOfWorkGroup;
        }

        private List<ProjectScopeOfWork> GetScopeOfWorkListFromTemplateFile(string templatePath)
        {
            string[] lines = File.ReadAllLines(templatePath);
            var scopeOfWorkList = new List<ProjectScopeOfWork>();
            int order = 0;
            foreach (string line in lines)
            {
                scopeOfWorkList.Add(new ProjectScopeOfWork() {Order = order, Description = line, IsInclude = true, Remarks = ""});
                order++;
            }

            return scopeOfWorkList;
        }
    }
}
