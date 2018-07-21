using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DbfReader;
using MigrationTool.DbModel;
using MigrationTool.Dto;
using Newtonsoft.Json;

namespace MigrationTool.Seeder
{
    public class ProjectSeeder
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public ProjectSeeder(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new HttpClient();
            InitializeHttpClient(baseUrl);
        }

        /// <summary>
        /// Seeds the specified main database file.
        /// </summary>
        public async Task<List<MainMaterialDto>> Seed(string mainDbFile, int projectId, string materialType)
        {
            List<MainMaterialDto> mainMaterials = new List<MainMaterialDto>();
            var dbf = DbfReaderUtil.OpenTable(mainDbFile);
            int lastMainDigit = 0;
            int lastSubDigit = 0;
            int lastMainGroupId = 0;
            int lastSubGroupId = 0;
            foreach (var row in dbf)
            {
                try
                {
                    var material = ProjectMaterialDbModel.CreateMaterialFromRow(row);
                    if (material.IsValid())
                    {
                        if (material.MainItemDigit > lastMainDigit)
                        {
                            lastMainDigit = material.MainItemDigit;
                            lastSubDigit = 0;
                            // Add Main Material Group
                            material.SanitizeName();
                            Console.WriteLine($"Main - {material.Name}");
                            var projectMaterialGroup = new ProjectMaterialGroupIncomingDto()
                            {
                                GroupCode = "",
                                GroupName = material.Name,
                                MaterialType = materialType,
                                ParentGroupId = null,
                                Remarks = material.Remark
                            };
                            lastMainGroupId = await CreateProjectMaterialGroup(projectId, projectMaterialGroup);
                            lastSubGroupId = 0;
                        }
                        else if (material.SubItemDigit > lastSubDigit)
                        {
                            lastSubDigit = material.SubItemDigit;
                            // Add sub material group
                            material.SanitizeName();
                            Console.WriteLine($"Sub - {material.Name}");
                            var projectMaterialGroup = new ProjectMaterialGroupIncomingDto()
                            {
                                GroupCode = "",
                                GroupName = material.Name,
                                MaterialType = materialType,
                                ParentGroupId = lastMainGroupId,
                                Remarks = material.Remark
                            };
                            lastSubGroupId = await CreateProjectMaterialGroup(projectId, projectMaterialGroup);
                        }
                        else
                        {
                            // Add Material to last group
                            Console.WriteLine($"Material - {material.Name}");
                            ProjectMaterialIncomingDto projectMaterialIncomingDto = new ProjectMaterialIncomingDto()
                            {
                                Name = material.Name,
                                Description = material.Description,
                                Class = MaterialClass.MainEquipment,
                                Quantity = material.Quantity,
                                CodeAsString = material.CodeAsString,
                                ListPrice = material.ListPrice,
                                NetPrice = material.NetPrice,
                                OfferPrice = material.OfferPrice,
                                Manpower = material.Manpower,
                                Fittings = material.Fittings,
                                Accessory = material.Accessory,
                                Supporting = material.Supporting,
                                Painting = material.Painting,
                                Remark = material.Remark,
                                Unit = material.Unit
                            };
                            if (lastSubGroupId > 0)
                                await CreateProjectMaterial(lastSubGroupId, projectMaterialIncomingDto);
                            else if (lastMainGroupId > 0)
                                await CreateProjectMaterial(lastMainGroupId, projectMaterialIncomingDto);
                        }

                        //var mainMaterialIncommingDto =
                        //    MainMaterialIncommingDto.CreateMainMaterialIncommingDtoFromMaterialDbModel(material,
                        //        _materialType);
                        //int mainMaterialId = await CreateMainMaterial(mainMaterialIncommingDto);
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error at file: {mainDbFile}, record {row["CODE"].GetString().Trim()}");
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return mainMaterials;
        }

        private async Task<int> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroupIncomingDto content)
        {
            HttpResponseMessage response = await _client.PostAsync($"api/project/group/{projectId}", new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new WebException(response.ToString());
            MainMaterialResponseDto result = JsonConvert.DeserializeObject<MainMaterialResponseDto>(await response.Content.ReadAsStringAsync());
            return result.Result.Id;
        }

        private async Task<int> CreateProjectMaterial(int projectMaterialGroupId, ProjectMaterialIncomingDto content)
        {
            HttpResponseMessage response = await _client.PostAsync($"api/ProjectMaterial/{projectMaterialGroupId}", new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new WebException(response.ToString());
            MainMaterialResponseDto result = JsonConvert.DeserializeObject<MainMaterialResponseDto>(await response.Content.ReadAsStringAsync());
            return result.Result.Id;
        }

        private void InitializeHttpClient(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
