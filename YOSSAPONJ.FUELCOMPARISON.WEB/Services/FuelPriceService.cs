using Newtonsoft.Json;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;
using static YOSSAPONJ.FUELCOMPARISON.WEB.Models.FuelPriceModel;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services
{
    public class FuelPriceService : IFuelPriceService
    {
        private HttpClient _httpClient;
        private FuelPriceModel _fuelPrice;
#if DEBUG
        private readonly string _baseURL = "https://localhost:7292/Proxy";
#else
        private readonly string _baseURL = "https://expensemanagerapi-op3eh0d6.b4a.run/Proxy";
#endif
        public FuelPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FuelPriceModel> GetAllFuelPrice()
        {
            if (_fuelPrice == null)
            {
                FuelPriceModel result = new FuelPriceModel();

                HttpResponseMessage response = await _httpClient.GetAsync(_baseURL + "/FuelPrice");
                
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<FuelPriceModel>(content);
                }

                _fuelPrice = result;
            }

            return _fuelPrice;
        }

        public async Task<StationModel> GetFuelPriceByStationName(string stationName)
        {
            var fuelPrice = await GetAllFuelPrice();

            return fuelPrice?.Stations?.Where(x => x.Name == stationName).FirstOrDefault();
        }
    }
}
