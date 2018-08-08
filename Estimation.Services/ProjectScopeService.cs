using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.HtmlToPdf.Domain;

namespace Estimation.Services
{
    public class ProjectScopeService : IProjectScopeService
    {
        private string ElectricMaterialTypeName => "Electrical";

        private string MechanicMaterialTypeName => "Mechanical";

        private string ElectricScopeOfWorkTemplatePath => @".\ProgramData\ScopeOfWork_Electrical.txt";

        private string MechanicScopeOfWorkTemplatePath => @".\ProgramData\ScopeOfWork_Mechanical.txt";

        private string ScopeOfWorkFormPath => "Forms/ScopeOfWork.html";

        private readonly IPdfService _pdfService;

        public ProjectScopeService(IPdfService pdfService)
        {
            _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
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
        public async Task<byte[]> GetProjectScopeOfWorkReport(ProjectScopeOfWorkGroup projectScopeOfWorkGroup)
        {
            string htmlTemplate = File.ReadAllText(ScopeOfWorkFormPath);
            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;

            HtmlParser.ParseHtmlNodeByClass(root, projectScopeOfWorkGroup);

            var printOrder = new PrintOrderRequest(){IsPortrait = true, Paper = PaperKind.A4};
            return await _pdfService.ExportProjectToPdf(new string[]{ root.OuterHtml }, printOrder);
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
