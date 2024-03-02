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
                double difResult = CalculatePercentageDifference(E10Detail.PriceToday, E20Detail.PriceToday);
                
                if (difResult < 6.71 && (E10Detail.PriceToday > E20Detail.PriceToday))
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
                    if (E10Detail.PriceToday > E20Detail.PriceToday)
                    {
                        result = new FuelSuggestionResultModel()
                        {
                            DifPercentage = difResult,
                            RecommendOilFullName = GASOHOL95_E20,
                            RecommendOilName = "E20",
                            OilDetail = E20Detail,
                        };
                    }
                    else
                    {
                        result = new FuelSuggestionResultModel()
                        {
                            DifPercentage = difResult,
                            RecommendOilFullName = GASOHOL95_E10,
                            RecommendOilName = "Gasohol 95 (Abnormal E20 Price)",
                            OilDetail = E10Detail,
                        };
                    }
                }
            }

            return result;
        }

        private async Task<BangchakOilListModel?> GetOilDetail(string oilName)
        {
            var oilList = await _oilPriceService.GetOilList();

            return oilList.Where(x => x.OilName == oilName).FirstOrDefault();
        }

        private double CalculatePercentageDifference(double value1, double value2)
        {
            double absoluteDifference = Math.Abs(value1 - value2);
            double average = (value1 + value2) / 2;

            return (absoluteDifference / average) * 100;
        }
    }

}