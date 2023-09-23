namespace YOSSAPONJ.FUELCOMPARISON.WEB.Models
{
    public class FuelSuggestionResultModel
    {
        public double DifPercentage { get; set; }
        public string RecommendOilName { get; set; }
        public string RecommendOilFullName { get; set; }
        public BangchakOilListModel OilDetail { get; set; }
    }
}
