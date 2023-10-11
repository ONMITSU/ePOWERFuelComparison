using Microsoft.AspNetCore.Components;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages
{
    public class DisplayCaltexFuelBase : ComponentBase
    {
        [Inject]
        public ICaltexFuelPriceService CaltexFuelPriceService { get; set; }
        public CaltexFuelPriceModel FuelPrice { get; set; }
        public List<CaltexFuelListModel> FuelList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FuelPrice = await CaltexFuelPriceService.GetFuelPrice();
            FuelList = FuelPrice.Fuels.Where(x => !x.FuelName.ToUpper().Contains("DIESEL")).ToList();
        }
    }
}
