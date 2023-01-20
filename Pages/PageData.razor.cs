using Infrastructuur.Dtos;
using Infrastructuur.Models;
using Infrastructuur.Pdfs;
using Microsoft.AspNetCore.Components;
using WebScrapperPdf.Services.Interfaces;

namespace WebScrapperPdf.Pages
{
    public partial class PageData : ComponentBase
    {

        [Inject]
        public IDataService DataService { get; set; }

        public ResultDto Result { get; set; } = new ResultDto();
        public static List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();
        public WebsiteEntity Website { get; set; } = new WebsiteEntity();

        private string title = "Page";
        public List<string> ButtonList { get; set; } = new List<string>();
        private async Task HandleSubmit()
        {

            Pdf.File.Images.Clear();
            Pdf.File.Hrefs.Clear();
            Pdf.File.Content.Clear();
            Pdf.File.Title = string.Empty;
            Data = new List<Dictionary<string, string>>();
            if (!string.IsNullOrEmpty(Website.Url))
            {
                title = Website.Url;
                Result = (await DataService.GetDataByTitleAsync(Website.Url));

                Result.Data = Result.Data
                 .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
                var dictionaryToAdd = new Dictionary<string, string>();
                int cc = 0;
                foreach (var node in HtmlTag.htmlNodes)
                {
                    foreach (var item in Result.Data)
                    {
                        if (item.Key.Contains(node))
                        {

                            dictionaryToAdd.Add(node + "-" + cc, item.Value);
                        }
                        cc++;
                    }
                    Data.Add(dictionaryToAdd);
                    cc = 0;
                    dictionaryToAdd = new Dictionary<string, string>();
                }
                ButtonList = Result.Data.Select(x => x.Key)
                    .Distinct()
                    .ToList();
            }
        }
    }
}
