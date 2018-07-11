using System;
using System.Collections.Generic;
using System.IO;
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
    public class MaterialSeeder
    {
        private readonly HttpClient _client;

        public MaterialSeeder(string baseUrl)
        {
            _client = new HttpClient();
            InitializeHttpClient(baseUrl);
        }

        public async Task Seed(string searchFolder, string dbFile,string subMaterialId)
        {
            if (FileHelper.SkipList.Contains(dbFile))
                return;

            var files = Directory.GetFiles(searchFolder, $"{dbFile}.DBF");
            if (files.Length != 1)
            {
                FileNotFoundLog.WriteLog(dbFile);
                return;
            }
            var file = files.First();

            Console.WriteLine($"***{file}***");
            List<Material> materials = new List<Material>();
            var dbf = DbfReaderUtil.OpenTable(file);
            foreach (var row in dbf)
            {
                try
                {
                    var material = Material.CreateMaterialFromRow(row);
                    materials.Add(material);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error at file: {file}, record {row["CODE"].GetString().Trim()}");
                    Console.WriteLine(e.Message);
                    throw;
                }
            }


            foreach (var material in materials)
            {
                try
                {
                    //Console.WriteLine(JsonConvert.SerializeObject(material, Formatting.Indented));
                    if (!material.IsValid())
                        continue;
                    
                    await CreateMaterial(subMaterialId, JsonConvert.SerializeObject(material, Formatting.Indented));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(JsonConvert.SerializeObject(material, Formatting.Indented));
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            Console.WriteLine($"Success!! SubMaterial ID = {subMaterialId}, File = {dbFile}");
        }

        private void InitializeHttpClient(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task CreateMaterial(string subMaterial, string content)
        {
            HttpResponseMessage response = await _client.PostAsync($"api/material/product/{subMaterial}", new StringContent(content, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new WebException(response.ToString());
        }
    }
}
