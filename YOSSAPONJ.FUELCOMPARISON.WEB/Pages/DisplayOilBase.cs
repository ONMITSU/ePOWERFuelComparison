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

        public BangchakOilPriceModel OilPriceDetail { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var oilPrice = await BangchakOilPriceService.GetOilPrice();
            var oilList = await BangchakOilPriceService.GetOilList();

            Oils = oilList.Where(x => x.OilName.ToUpper().Contains("GASOHOL")).ToArray();
            OilPriceDetail = oilPrice.FirstOrDefault();
        }
    }
}
