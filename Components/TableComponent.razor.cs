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
                Pdf.File.Images.Add(new PdfContent<string, string> { Key = item.Key, Value = item.Value });
            }
            else if (item.Key.StartsWith("h1"))
            {
                Pdf.File.Title = item.Value;
            }
            else if (item.Key.StartsWith("a"))
            {
                Pdf.File.Hrefs.Add(new PdfContent<string, string> { Key = item.Key, Value = item.Value });
            }
            else
            {
                Pdf.File.Content.Add(new PdfContent<string, string> { Key = item.Key, Value = item.Value });
            }
        }
        public void Remove(KeyValuePair<string, string> item)
        {
            PdfContent<string, string> pdf = new PdfContent<string, string> { Key = item.Key, Value = item.Value };
            if (item.Key.StartsWith("img"))
            {
                //Pdf.File.Images.Remove(Pdf.File.Images.FirstOrDefault(k => k.Key == pdf.Key));
                for (int i = 0; i < Pdf.File.Images.Count; i++)
                {
                    if (Pdf.File.Images[i].Key == pdf.Key)
                    {
                        Pdf.File.Images.RemoveAt(i);
                    }
                }
            }
            else if (item.Key.StartsWith("h1"))
            {
                Pdf.File.Title = string.Empty;
            }
            else if (item.Key.StartsWith("a"))
            {
                //Pdf.File.Hrefs.Remove(Pdf.File.Images.FirstOrDefault(k => k.Key == pdf.Key));
                for (int i = 0; i < Pdf.File.Hrefs.Count; i++)
                {
                    if (Pdf.File.Hrefs[i].Key == pdf.Key)
                    {
                        Pdf.File.Hrefs.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Pdf.File.Content.Count; i++)
                {
                    if (Pdf.File.Content[i].Key == pdf.Key)
                    {
                        Pdf.File.Content.RemoveAt(i);
                    }
                }
            }
        }
    }
}
