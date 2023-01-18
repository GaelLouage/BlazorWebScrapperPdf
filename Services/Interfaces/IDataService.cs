using Infrastructuur.Dtos;
using Infrastructuur.Models;

namespace WebScrapperPdf.Services.Interfaces
{
    public interface IDataService
    {
        Task<ResultDto> GetDataByTitleAsync(string title);
        Task<byte[]> DownloadPdfFileAsync(PdfEntity pdfFile);
    }
}