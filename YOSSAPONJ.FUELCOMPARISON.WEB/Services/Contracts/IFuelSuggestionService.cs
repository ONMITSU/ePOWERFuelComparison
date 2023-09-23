using YOSSAPONJ.FUELCOMPARISON.WEB.Models;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts
{
    public interface IFuelSuggestionService
    {
        public Task<FuelSuggestionResultModel> GetSuggestion();
    }
}
