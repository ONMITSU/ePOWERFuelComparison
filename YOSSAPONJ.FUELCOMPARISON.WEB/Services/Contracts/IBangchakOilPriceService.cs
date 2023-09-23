using YOSSAPONJ.FUELCOMPARISON.WEB.Models;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface IBangchakOilPriceService
    {
        public Task<BangchakOilPriceModel[]> GetOilPrice();
        public Task<BangchakOilListModel[]> GetOilList();
    }
}
