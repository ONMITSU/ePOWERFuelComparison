using YOSSAPONJ.FUELCOMPARISON.WEB.Models;
using YOSSAPONJ.FUELCOMPARISON.WEB.Services.Contracts;

namespace YOSSAPONJ.FUELCOMPARISON.WEB.Services
{
    public class FuelSuggestionService : IFuelSuggestionService
    {
        private IBangchakOilPriceService _oilPriceService;

        public FuelSuggestionService(IBangchakOilPriceService oilPriceService) 
        {
            _oilPriceService = oilPriceService;
        }

        public async Task<FuelSuggestionResultModel> GetSuggestion()
        {
            FuelSuggestionResultModel result = new FuelSuggestionResultModel()
            {
              DifPercentage = 0.0,
              RecommendOilFullName = "(No Data)",
              RecommendOilName = "(No Data)",
              OilDetail = new BangchakOilListModel()
            };

            const string GASOHOL95_E10 = "Gasohol 95 S EVO";
            const string GASOHOL95_E20 = "Gasohol E20 S EVO";

            BangchakOilListModel E10Detail = await GetOilDetail(GASOHOL95_E10);
            BangchakOilListModel E20Detail = await GetOilDetail(GASOHOL95_E20);

            if (E10Detail != null && E20Detail != null)
            {
                double difResult = ((E10Detail.PriceToday - E20Detail.PriceToday) / E10Detail.PriceToday) * 100;
                
                if (difResult < 6.71)
                {
                    result = new FuelSuggestionResultModel(){
                        DifPercentage = difResult,
                        RecommendOilFullName = GASOHOL95_E10,
                        RecommendOilName = "Gasohol 95",
                        OilDetail = E10Detail,
                        
                    };
                }
                else
                {
                    result = new FuelSuggestionResultModel()
                    {
                        DifPercentage = difResult,
                        RecommendOilFullName = GASOHOL95_E20,
                        RecommendOilName = "E20",
                        OilDetail = E20Detail,
                    };
                }
            }

            return result;
        }

        private async Task<BangchakOilListModel?> GetOilDetail(string oilName)
        {
            var oilList = await _oilPriceService.GetOilList();

            return oilList.Where(x => x.OilName == oilName).FirstOrDefault();
        }

    }
}
