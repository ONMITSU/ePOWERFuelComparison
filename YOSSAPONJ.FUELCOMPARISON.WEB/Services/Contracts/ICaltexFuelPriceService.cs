using YOSSAPONJ.FUELCOMPARISON.WEB.Models;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface ICaltexFuelPriceService
    {
        Task<CaltexFuelPriceModel> GetFuelPrice();
    }
}
