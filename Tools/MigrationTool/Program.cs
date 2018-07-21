#define project
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
using MigrationTool.Seeder;
using Newtonsoft.Json;

namespace MigrationTool
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        static async Task Main(string[] args)
        {
            string baseUrl = "http://localhost:8989/";

#if material
            // Electric
            //const int mainLimit = int.MaxValue;
            //string materialType = "Electric";
            //string mainMaterialFilePath = @"C:\Estimate2\ELECTRIC.MAT";
            //string materialFolderPath = @"C:\Estimate2\MATERIAL\ELECTRIC";

            // Mechanic
            //const int mainLimit = 7;
            //string materialType = "Mechanic";
            //string mainMaterialFilePath = @"C:\Estimate2\MECHANIC.MAT";
            //string materialFolderPath = @"C:\Estimate2\MATERIAL\MECHANIC";

            // Computer
            const int mainLimit = 35;
            string materialType = "Computer";
            string mainMaterialFilePath = @"C:\Estimate2\COMPUTER.MAT";
            string materialFolderPath = @"C:\Estimate2\MATERIAL\COMPUTER";

            try
            {
                MainMaterialSeeder mainMaterialSeeder = new MainMaterialSeeder(baseUrl, materialType);
                var mainMaterials = await mainMaterialSeeder.Seed(mainMaterialFilePath, mainLimit, 0);
                MaterialSeeder materialSeeder = new MaterialSeeder(baseUrl);

                foreach (var mainMaterialDto in mainMaterials)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(mainMaterialDto.MainMaterialIncommingDto, Formatting.Indented));
                    foreach (var subMaterialDto in mainMaterialDto.SubMaterialDtos)
                    {
                        await materialSeeder.Seed(materialFolderPath, subMaterialDto.DbFileName, subMaterialDto.SubMaterialId);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
#endif
#if project
            string projectFilePath = @"C:\Estimate2\DATA\ELECTRIC\SAMPLE.DBF";
            try
            {
                ProjectSeeder projectSeeder = new ProjectSeeder(baseUrl);
                var mainMaterials = await projectSeeder.Seed(projectFilePath, 1, "Electric");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
#endif
            Console.WriteLine("Finish!! Press any key to exit...");
            Console.ReadKey();
        }

        private static async Task Old()
        {
            string searchFolder = @"C:\Estimate\Estimate\MATERIAL\ELECTRIC";
            string searchPattern = "M916S*.DBF";
            Dictionary<int, string> inputFileMap = new Dictionary<int, string>()
            {
                { 72, "01" },
                { 73, "02" },
                { 74, "03" },
                { 75, "04" }
            };

            Dictionary<int, string> fileMap = new Dictionary<int, string>();
            foreach (var keyValuePair in inputFileMap)
            {
                fileMap.Add(keyValuePair.Key, searchPattern.Replace("*", keyValuePair.Value));
            }

            InitializeHttpClient("http://localhost:8989/");

            foreach (var idFile in fileMap)
            {
                var files = Directory.GetFiles(searchFolder, idFile.Value);
                if (files.Length != 1)
                    throw new Exception($"Found file {files.Length}");
                var file = files.First();

                Console.WriteLine($"***{file}***");
                List<Material> materials = new List<Material>();
                var dbf = DbfReaderUtil.OpenTable(file);
                foreach (var row in dbf)
                {
                    try
                    {
                        var material = GetMaterialFromRow(row);
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
                        await CreateMaterial(idFile.Key.ToString(), JsonConvert.SerializeObject(material, Formatting.Indented));
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(JsonConvert.SerializeObject(material, Formatting.Indented));
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }

                Console.WriteLine($"Success!! SubMaterial ID = {idFile.Key}, File = {idFile.Value}");
            }

            Console.ReadKey();
        }


        private static Material GetMaterialFromRow(IDbfRow row)
        {
            Material material = new Material()
            {
                CodeAsString = row["CODE"]
                    .GetString().Trim(),
                Name = row["MATERIAL"]
                    .GetString()
                    .Trim(),
                Description = row["DESCRIPT"]
                    .GetString().Trim(),
                Unit = string.IsNullOrWhiteSpace(row["UNIT"].GetString())
                    ? "LOT"
                    : row["UNIT"]
                        .GetString().Trim(),
                ListPrice = row["LISTPRICE"].GetDecimal(),
                NetPrice = row["NETPRICE"].GetDecimal(),
                OfferPrice = row["ESTPRICE"].GetDecimal(),
                Manpower = string.IsNullOrWhiteSpace(row["MANPOWER"].ForceString())
                    ? 0
                    : row["MANPOWER"].GetDecimal(),
                Accessory = row["ACCESSORY"].GetDecimal(),
                Supporting = row["SUPPORT"].GetDecimal(),
                Painting = row["PAINTING"].GetDecimal(),
                Remark = row["REMARKS"].GetString().Trim()
            };
            var codes = material.CodeAsString.Split("-");
            material.Code = Int16.Parse(codes[2]);
            return material;
        }

        private static async Task CreateMaterial(string subMaterial, string content)
        {
            HttpResponseMessage response = await Client.PostAsync($"api/material/product/{subMaterial}", new StringContent(content, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw new WebException(response.ToString());
        }

        private static void InitializeHttpClient(string baseAddress)
        {
            Client.BaseAddress = new Uri(baseAddress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
