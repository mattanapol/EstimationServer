using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DbfReader;
using Newtonsoft.Json;

namespace MigrationTool
{
    public class MainMaterialSeeder
    {
        private readonly HttpClient _client;
        private readonly string _materialType;
        private readonly string _baseUrl;

        public MainMaterialSeeder(string baseUrl, string materialType)
        {
            _baseUrl = baseUrl;
            _materialType = materialType;
            _client = new HttpClient();
            InitializeHttpClient(baseUrl);
        }

        /// <summary>
        /// Seeds the specified main database file.
        /// </summary>
        /// <param name="mainDbFile">The main database file.</param>
        /// <param name="take">The take.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public async Task<List<MainMaterialDto>> Seed(string mainDbFile, int take, int skip)
        {
            List<MainMaterialDto> mainMaterials = new List<MainMaterialDto>();
            var dbf = DbfReaderUtil.OpenTable(mainDbFile).Skip(skip).Take(take);
            foreach (var row in dbf)
            {
                try
                {
                    var material = MaterialDbModel.CreateMaterialDbModelFromRow(row);
                    if (material.HasSub)
                    {
                        var mainMaterialIncommingDto =
                            MainMaterialIncommingDto.CreateMainMaterialIncommingDtoFromMaterialDbModel(material,
                                _materialType);
                        int mainMaterialId = await CreateMainMaterial(mainMaterialIncommingDto);
                        mainMaterials.Add(new MainMaterialDto()
                        {
                            MainMaterialIncommingDto = mainMaterialIncommingDto,
                            SubMaterialDtos = new List<SubMaterialDto>(),
                            MainMaterialId = mainMaterialId.ToString()
                        });
                    }
                    else
                    {
                        var subMaterialIncommingDto =
                            SubMaterialIncommingDto.CreateSubMaterialIncommingDtoFromMaterialDbModel(material);
                        int subMaterialId = await CreateSubMaterial(mainMaterials.Last().MainMaterialId,
                            subMaterialIncommingDto);
                        var subMaterial = new SubMaterialDto()
                        {
                            SubMaterialIncommingDto = subMaterialIncommingDto,
                            DbFileName = material.DatabaseFileName,
                            SubMaterialId = subMaterialId.ToString()
                        };

                        mainMaterials.Last().SubMaterialDtos.Add(subMaterial);
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

        private async Task<int> CreateMainMaterial(MainMaterialIncommingDto content)
        {
            HttpResponseMessage response = await _client.PostAsync($"api/material/main", new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new WebException(response.ToString());
            MainMaterialResponseDto result = JsonConvert.DeserializeObject<MainMaterialResponseDto>(await response.Content.ReadAsStringAsync());
            return result.Result.Id;
        }

        private async Task<int> CreateSubMaterial(string mainMaterialId, SubMaterialIncommingDto content)
        {
            HttpResponseMessage response = await _client.PostAsync($"api/material/sub/{mainMaterialId}", new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented), Encoding.UTF8, "application/json"));
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
