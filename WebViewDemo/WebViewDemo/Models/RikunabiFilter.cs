using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;

namespace WebViewDemo.Models
{
    public class RikunabiFilter : IDisposable
    {
        private readonly HttpClient _Client = new HttpClient();
        private readonly IHtmlParser _Parser = new HtmlParser();

        public string FilterWord { get; set; } = "常駐";

        public async Task<string> FilterAsync(string source)
        {
            using (var document = await _Parser.ParseDocumentAsync(source))
            {
                var companies = document.QuerySelectorAll(".rnn-jobOfferList__item");

                // 今は2つだけ
                for (int i = 0; i < 10; i++)
                {
                    var suffix = companies[i].QuerySelector(".rnn-textLl .rnn-linkText").GetAttribute("href");
                    var companyUri = "https://next.rikunabi.com" + suffix;
                    var encoding = Encoding.GetEncoding("shift_jis");
                    var companyHtml = await GetStringAsync(companyUri, encoding);
                    var companyDocument = await _Parser.ParseDocumentAsync(companyHtml);
                    var tab = companyDocument.QuerySelectorAll(".rnn-tabMenu__navi__itemlink").First(x => x.QuerySelector("span").TextContent == "求人情報");

                    suffix = tab.GetAttribute("href");

                    if (suffix != null)
                    {
                        companyHtml = await GetStringAsync(companyUri, encoding);
                        companyDocument = await _Parser.ParseDocumentAsync(companyHtml);
                    }

                    companyHtml = companyDocument.QuerySelector(".rnn-offerInfoMain").OuterHtml;

                    if (companyHtml.Contains(FilterWord))
                    {
                        companies[i].Remove();
                    }
                }

                return document.DocumentElement.OuterHtml;
            }
        }

        public void Dispose() => _Client?.Dispose();

        private async Task<string> GetStringAsync(string requestUri, Encoding encoding)
        {
            var responseMessage = await _Client.GetAsync(requestUri);

            responseMessage.EnsureSuccessStatusCode();

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, encoding, true) as TextReader)
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
