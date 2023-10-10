namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface ICaltexFuelPriceService
    {
        Task<string> GetFuelPrice();
    }
}
