using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;
using HtmlAgilityPack;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services
{
    public class CaltexFuelPriceService : ICaltexFuelPriceService
    {
        private HttpClient _httpClient;
        private CaltexFuelPriceModel _fuelPrice;
        private readonly string _baseURL = "https://www.caltex.com";
        public CaltexFuelPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CaltexFuelPriceModel> GetFuelPrice()
        {
            if (_fuelPrice == null)
            {
                string html = await GetRawPage();
                _fuelPrice = await ParseHtmlToModel(html);
            }

            return _fuelPrice;
        }

        #region Private Function
        private async Task<string> GetRawPage()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://corsproxy.io/?" + _baseURL + "/th/motorists/products-and-services/fuel-prices.html");
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<CaltexFuelPriceModel> ParseHtmlToModel(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection fuelPriceTable = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, \"price-item\")]");
            string fuelPriceLastUpdate = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, \"info-text\")]//p").InnerHtml;

            CaltexFuelPriceModel caltexFuelPrice = new CaltexFuelPriceModel();

            caltexFuelPrice.LastFetched = DateTime.Now;
            caltexFuelPrice.LastUpdateText = fuelPriceLastUpdate;
            caltexFuelPrice.LastUpdate = ConvertUpdateTextToDateTime(fuelPriceLastUpdate);
            caltexFuelPrice.Fuels = new List<CaltexFuelListModel>();

            foreach (var detail in fuelPriceTable)
            {
                CaltexFuelListModel model = new CaltexFuelListModel();

                HtmlNode[] fuel = detail.SelectNodes(".//div[contains(@class, \"wrapper\")]//p").ToArray();
                string fuelIcon = detail.SelectSingleNode(".//div[contains(@class, \"wrapper\")]//span//img").GetAttributeValue("src", "");

                for (int i = 0; i < fuel.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            // Fuel Name
                            model.FuelName = fuel[0].InnerHtml;
                            break;
                        case 1:
                            // Fuel Price
                            model.FuelPrice = ConvertPriceToDouble(fuel[1].InnerHtml);
                            break;
                        default:
                            break;
                    }
                }
                model.Icon = _baseURL + fuelIcon;

                caltexFuelPrice.Fuels.Add(model);
            }

            return caltexFuelPrice;
        }
        private DateTime ConvertUpdateTextToDateTime(string updateText)
        {
            DateTime result;
            updateText = updateText.Replace("ปรับปรุงล่าสุด ", string.Empty);

            if (!DateTime.TryParseExact(
                updateText, 
                "MMMM dd, yyyy hh:mmtt", 
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, 
                out result))
            {
                result = DateTime.Now;
            }
            return result;
        }
        private double ConvertPriceToDouble(string price)
        {
            double result;
            price = price.Replace("BHT ", string.Empty);

            if (!double.TryParse(price, out result)) result = 0;
            return result;
        }
        #endregion Private Function
    }
}
