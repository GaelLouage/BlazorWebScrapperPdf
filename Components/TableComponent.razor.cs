using Infrastructuur.Dtos;
using Infrastructuur.Models;
using Infrastructuur.Pdfs;
using Microsoft.AspNetCore.Components;
using WebScrapperPdf.Services.Interfaces;

namespace WebScrapperPdf.Components
{
    public partial class TableComponent : ComponentBase
    {
        [Parameter]
        public Dictionary<string, string>? Items { get; set; } = new Dictionary<string, string>();

        [Parameter]
        public string TheKey { get; set; }

        public string GetHtmlNode() => Items.FirstOrDefault(x => x.Key.Contains(TheKey)).Key is not null ?
                    Items.FirstOrDefault(x => x.Key.Contains(TheKey)).Key.Split('-')[0] : "";
      
        private string TableContent = "table-content-hide";
        private bool tableIsClicked = false;
        public void DisplayTable()
        {
            tableIsClicked = !tableIsClicked;
            if (tableIsClicked)
            {
                TableContent = "table-content-show";
            }
            else
            {
                TableContent = "table-content-hide";
            }
        }
        public void AddItem(KeyValuePair<string, string> item)
        {
            if (item.Key.StartsWith("img"))
            {
                Pdf.File.Images.TryAdd(item.Key, item.Value);
            }
            else if (item.Key.StartsWith("h1"))
            {
                Pdf.File.Title = item.Value;
            }
            else if (item.Key.StartsWith("a"))
            {
                Pdf.File.Hrefs.TryAdd(item.Key, item.Value);
            }
            else
            {
                Pdf.File.Content.TryAdd(item.Key, item.Value);
            }
        }
        public void Remove(KeyValuePair<string, string> item)
        {
            if (item.Key.StartsWith("img"))
            {
                Pdf.File.Images.Remove(item.Key);
            }
            else if (item.Key.StartsWith("h1"))
            {
                Pdf.File.Title = item.Value;
            }
            else if (item.Key.StartsWith("a"))
            {
                Pdf.File.Hrefs.Remove(item.Key);
            }
            else
            {
                Pdf.File.Content.Remove(item.Key);
            }
        }
    }
}
