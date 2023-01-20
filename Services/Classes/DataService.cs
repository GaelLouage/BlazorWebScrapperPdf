using Infrastructuur.Dtos;
using Infrastructuur.Models;
using Microsoft.JSInterop;
using System.Reflection.Metadata;
using System;
using System.Text;
using System.Text.Json;
using WebScrapperPdf.Services.Interfaces;
using System.IO;


namespace WebScrapperPdf.Services.Classes
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultDto> GetDataByTitleAsync(string title)
        {
            return await JsonSerializer.DeserializeAsync<ResultDto>
             (await _httpClient.GetStreamAsync($"https://localhost:7152/api/WebScrapper/GetAllDataFromName/{title}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<byte[]> DownloadPdfFileAsync(PdfEntity pdfFile)
        {

            var pdfData =
              new StringContent(JsonSerializer.Serialize(pdfFile), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://localhost:7152/api/Pdf", pdfData);

            return await response.Content.ReadAsByteArrayAsync();


        }
    }
}
