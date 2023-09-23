using Newtonsoft.Json;
using System.Net.Http;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services
{
    public class BangchakOilPriceService : IBangchakOilPriceService
    {
        private HttpClient _httpClient;
        private BangchakOilListModel[] _oilList;

        public BangchakOilPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BangchakOilPriceModel[]> GetOilPrice()
        {
            BangchakOilPriceModel[] result = new BangchakOilPriceModel[0];

            HttpResponseMessage response = await _httpClient.GetAsync("https://corsproxy.io/?https://oil-price.bangchak.co.th/ApiOilPrice2/en");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<BangchakOilPriceModel[]>(content);
                
                //Console.WriteLine("Response Content: " + content);
                //Console.WriteLine(result);
            }
            else
            {
                //Console.WriteLine("Error: " + response.StatusCode);
            }

            return result;
        }

        public async Task<BangchakOilListModel[]> GetOilList()
        {
            if (_oilList == null)
            {
                var remoteOilPrice = await this.GetOilPrice();
                if (remoteOilPrice != null)
                {
                    _oilList = JsonConvert.DeserializeObject<BangchakOilListModel[]>(remoteOilPrice[0].OilList);

                    if (_oilList != null)
                    {
                        foreach (var oil in _oilList)
                        {
                            oil.PriceDifYesterday = oil.PriceToday - oil.PriceYesterday;
                            oil.PriceDifTomorrow = oil.PriceToday - oil.PriceTomorrow;
                        }
                    }
                }
            }

            return _oilList;
        }

    }
}
