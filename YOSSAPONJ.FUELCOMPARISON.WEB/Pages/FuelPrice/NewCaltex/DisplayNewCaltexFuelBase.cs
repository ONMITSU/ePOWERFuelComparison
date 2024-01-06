using Microsoft.AspNetCore.Components;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages.FuelPrice.NewCaltex
{
    public class DisplayNewCaltexFuelBase : ComponentBase
    {
        [Inject]
        public IFuelPriceService FuelPriceService { get; set; }
        public FuelPriceModel.StationModel StationInfo { get; set; }
        public Dictionary<string, double> FuelList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            StationInfo = await FuelPriceService.GetFuelPriceByStationName("Caltex");
            FuelList = StationInfo.FuelPrices.Where(x => !x.Key.Contains("Diesel") && x.Value != null)
                .Select(x => new KeyValuePair<string, double>(x.Key, x.Value.GetValueOrDefault()))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
