using Microsoft.AspNetCore.Components;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages.FuelPrice
{
    public class FuelPriceBase : ComponentBase
    {
        public Station SelectedStation { get; set; }

        public enum Station
        {
            Bangchak,
            Caltex,
            //OldCaltex,
            EPPO,
        }

        protected override async Task OnInitializedAsync()
        {
        }
    }
}
