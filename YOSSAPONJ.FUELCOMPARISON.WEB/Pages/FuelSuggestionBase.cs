using Microsoft.AspNetCore.Components;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages
{
    public class FuelSuggestionBase : ComponentBase
    {
        [Inject]
        public IBangchakOilPriceService BangchakOilPriceService { get; set; }

        [Inject]
        public IFuelSuggestionService FuelSuggestionService { get; set; }

        public FuelSuggestionResultModel SuggestionResult { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SuggestionResult = await FuelSuggestionService.GetSuggestion();
        }

    }
}
