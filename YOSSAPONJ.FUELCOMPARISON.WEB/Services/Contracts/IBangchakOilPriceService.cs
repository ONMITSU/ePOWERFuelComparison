using YOSSAPONJ.FUELCOMPARISON.WEB.Models;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface IBangchakOilPriceService
    {
        /// <summary>
        /// Get oil price
        /// </summary>
        /// <returns>Oil status data</returns>
        public Task<BangchakOilPriceModel[]> GetOilPrice();

        /// <summary>
        /// Get oil list
        /// </summary>
        /// <returns>Oil list</returns>
        public Task<BangchakOilListModel[]> GetOilList();
    }
}
