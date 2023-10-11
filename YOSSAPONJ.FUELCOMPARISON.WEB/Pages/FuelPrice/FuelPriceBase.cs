using Microsoft.AspNetCore.Components;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages.FuelPrice
{
    public class FuelPriceBase : ComponentBase
    {
        public Station SelectedStation { get; set; }

        public enum Station
        {
            Bangchak,
            Caltex
        }

        protected override async Task OnInitializedAsync()
        {
        }
    }
}
