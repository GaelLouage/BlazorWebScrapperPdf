using Infrastructuur.Extensions;
using Infrastructuur.Models;
using Infrastructuur.Pdfs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PdfSharpCore.Drawing;
using System.Net.Mime;
using WebScrapperPdf.Services.Interfaces;

namespace WebScrapperPdf.Pages
{
    public partial class PdfComponent : ComponentBase
    {
        [Inject]
        public IDataService DataService { get; set; }
        public List<XFontStyle> FontStyles = new List<XFontStyle>() {
            XFontStyle.Bold,
            XFontStyle.Italic,
            XFontStyle.Underline,
            XFontStyle.Strikeout,
            XFontStyle.Regular,
            XFontStyle.BoldItalic,
        };
   

        public List<string> FontFamilies = new List<string>()
        {
            "Arial", "Times New Roman","Verdana","Rockwell","Franklin Gothic","Univers","Frutiger","Avenir"
        };
        public List<string> Brushes = new List<string>
        {
            "Black", 
            "White",
            "Red",
            "Green",
            "Blue"
         };
        public async Task UpdateFile()
        {
            Pdf.File.TheFileBase64 = await DataService.DownloadPdfFileAsync(Pdf.File).GetBase64String();
        }
        protected async Task HandleValidSubmit()
        {
            Pdf.File.TheFileBase64 = await DataService.DownloadPdfFileAsync(Pdf.File).GetBase64String();
        }
        public void DisplayForm(string keyPressed, string nameOfProperty)
        {
            Pdf.File.Images.ResetItems();
            Pdf.File.Content.ResetItems();
            Pdf.File.Hrefs.ResetItems();
            switch (nameOfProperty)
            {
                case nameof(Pdf.File.Images):
                    var dataImages = Pdf.File.Images.FirstOrDefault(x=> x.Key== keyPressed);
                    if(dataImages is not null)
                    {
                        dataImages.IsClicked = !dataImages.IsClicked;
                    }
                    break;
                case nameof(Pdf.File.Content):
                    var dataContent = Pdf.File.Content.FirstOrDefault(x => x.Key == keyPressed);
                    if (dataContent is not null)
                    {
                        dataContent.IsClicked = !dataContent.IsClicked;
                    }
                    break;
                case nameof(Pdf.File.Hrefs):
                    var dataHrefs = Pdf.File.Hrefs.FirstOrDefault(x => x.Key == keyPressed);
                    if (dataHrefs is not null)
                    {
                        dataHrefs.IsClicked = !dataHrefs.IsClicked;
                    }
                    break;
            }
        }
    }
}
