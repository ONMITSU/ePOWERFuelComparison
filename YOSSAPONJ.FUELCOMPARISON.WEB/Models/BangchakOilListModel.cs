namespace YOSSAPONJ.FUELCOMPARISON.WEB.Models
{
    public class BangchakOilListModel
    {
        public string OilName { get; set; }
        public double PriceYesterday { get; set; }
        public double PriceToday { get; set; }
        public double PriceTomorrow { get; set; }
        public double PriceDifYesterday { get; set; }
        public double PriceDifTomorrow { get; set; }
        public string Icon { get; set; } //Image URL
        public string IconWeb { get; set; } //Image URL
        public string IconWeb2 { get; set; } //Image URL
    }
}
