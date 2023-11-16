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
        private BangchakOilPriceModel[] _oilPrice;
        private readonly string _baseProxy = "https://expensemanagerapi-op3eh0d6.b4a.run/Proxy?uri=";

        public BangchakOilPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BangchakOilPriceModel[]> GetOilPrice()
        {
            if (_oilPrice == null)
            {
                BangchakOilPriceModel[] result = new BangchakOilPriceModel[0];

                HttpResponseMessage response = await _httpClient.GetAsync(_baseProxy + "https://oil-price.bangchak.co.th/ApiOilPrice2/en");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<BangchakOilPriceModel[]>(content);
                    result.FirstOrDefault().LastFetched = DateTime.Now;
                }

                _oilPrice = result;
            }

            return _oilPrice;
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
                            oil.PriceDifYesterday = oil.PriceYesterday - oil.PriceToday;
                            oil.PriceDifTomorrow = oil.PriceTomorrow - oil.PriceToday;
                        }
                    }
                }
            }

            return _oilList;
        }

    }
}
