using Microsoft.AspNetCore.Components;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages.FuelPrice.EPPO
{
    public class DisplayEPPOBase : ComponentBase
    {
        [Inject]
        public IFuelPriceService FuelPriceService { get; set; }
        public FuelPriceModel PriceInfo { get; set; }
        public string SelectedStationA
        { 
            get { return this._selectedStationA; } 
            set
            {
                _selectedStationA = value;

                if (!string.IsNullOrEmpty(value) && PriceInfo != null)
                    StationA = PriceInfo.Stations.Where(x => x.Name == value).FirstOrDefault();
                else
                    StationA = null;
            } 
        }
        public string SelectedStationB
        { 
            get { return this._selectedStationB; }
            set 
            {
                _selectedStationB = value;

                if (!string.IsNullOrEmpty(value) && PriceInfo != null)
                    StationB = PriceInfo.Stations.Where(x => x.Name == value).FirstOrDefault();
                else
                    StationB = null;
            }
        }
        public FuelPriceModel.StationModel StationA { get; set; }
        public FuelPriceModel.StationModel StationB { get; set; }

        private string _selectedStationA = string.Empty;
        private string _selectedStationB = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            PriceInfo = await FuelPriceService.GetAllFuelPrice();
        }

        protected async Task OnResetStation()
        {
            SelectedStationB = string.Empty;
            SelectedStationA = string.Empty;
        }
    }
}
