using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;
using HtmlAgilityPack;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services
{
    public class CaltexFuelPriceService : ICaltexFuelPriceService
    {
        private HttpClient _httpClient;

        public CaltexFuelPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFuelPrice()
        {
            var html = await GetRawPage();
            var result = await ParseHtml(html);
            return "";
        }

        private async Task<string> GetRawPage()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://corsproxy.io/?https://www.caltex.com/th/motorists/products-and-services/fuel-prices.html");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<List<string>> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var fuelPriceTable = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, \"price-item\")]");

            foreach (var detail in fuelPriceTable)
            {
                var fuel = detail.SelectNodes(".//div[contains(@class, \"wrapper\")]//p").ToArray();
                var fuelIcon = detail.SelectSingleNode(".//div[contains(@class, \"wrapper\")]//span//img").GetAttributeValue("src", "");

                foreach (var data in fuel)
                {
                    var res = data.InnerHtml;
                    Console.WriteLine(res);
                }
            }

            return new List<string>();
        }

    }
}
