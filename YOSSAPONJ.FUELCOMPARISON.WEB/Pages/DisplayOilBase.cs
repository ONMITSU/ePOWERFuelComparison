using Microsoft.AspNetCore.Components;
using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Pages
{
    public class DisplayOilBase : ComponentBase
    {
        [Inject]
        public IBangchakOilPriceService BangchakOilPriceService { get; set; }

        public BangchakOilListModel[] Oils { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Oils = await BangchakOilPriceService.GetOilList();
            Oils = Oils.Where(x => x.OilName.ToUpper().Contains("GASOHOL")).ToArray();
        }
    }
}
