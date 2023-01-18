using Infrastructuur.Pdfs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebScrapperPdf.Services.Interfaces;

namespace WebScrapperPdf.Pages
{
    public partial class PdfComponent : ComponentBase
    {
        [Inject]
        public IDataService DataService { get; set; }
       
        public async Task DownloadPdfFile()
        {

            await JS.InvokeVoidAsync("saveAsFile", "tst", await DataService.DownloadPdfFileAsync(Pdf.File));

        }
    }
}
