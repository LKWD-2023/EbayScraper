using AngleSharp.Html.Parser;

namespace EbayScraper.Data
{
    public static class EbayScraper
    {
        public static List<EbayItem> Scrape(string searchTerm)
        {
            var html = GetEbayHtml(searchTerm);
            return ParseHtml(html);
        }

        private static string GetEbayHtml(string searchText)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            using var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");

            var url = $"https://www.ebay.com/sch/i.html?_nkw={searchText}";
            var html = client.GetStringAsync(url).Result;
            return html;
        }

        private static List<EbayItem> ParseHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            var divs = document.QuerySelectorAll(".s-item__wrapper");
            var items = new List<EbayItem>();
            foreach (var div in divs)
            {
                EbayItem item = new();
                items.Add(item);
                var titleElement = div.QuerySelector(".s-item__title");
                if (titleElement != null)
                {
                    item.Title = titleElement.TextContent;
                }

                var priceElement = div.QuerySelector(".s-item__price");
                if (priceElement != null)
                {
                    item.Price = priceElement.TextContent;
                }

                var imageWrapper = div.QuerySelector(".s-item__image-wrapper.image-treatment");
                if (imageWrapper != null)
                {
                    var image = imageWrapper.QuerySelector("img");
                    if(image != null)
                    {
                        item.Image = image.Attributes["src"].Value;
                    }
                }

                var aTag = div.QuerySelector("a.s-item__link");
                if(aTag != null)
                {
                    item.Url = aTag.Attributes["href"].Value;
                }
            }

            return items;
        }
    }
}