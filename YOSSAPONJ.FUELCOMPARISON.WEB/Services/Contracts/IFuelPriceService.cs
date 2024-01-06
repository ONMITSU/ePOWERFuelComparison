using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using static YOSSAPONJ.FUELCOMPARISON.WEB.Models.FuelPriceModel;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface IFuelPriceService
    {
        Task<FuelPriceModel> GetAllFuelPrice();
        Task<StationModel> GetFuelPriceByStationName(string stationName);
    }
}
