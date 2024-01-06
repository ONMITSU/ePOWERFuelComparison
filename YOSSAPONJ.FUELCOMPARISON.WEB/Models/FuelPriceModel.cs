namespace YOSSAPONJ.FUELCOMPARISON.WEB.Models
{
    public class FuelPriceModel
    {
        public StationModel[]? Stations { get; set; }
        public DateTime? LastFetched { get; set; }

        public class StationModel
        {
            public string? Name { get; set; }
            public string? IconURI { get; set; }
            public Dictionary<string, double?>? FuelPrices { get; set; }
            public DateTime? EffectiveDate { get; set; }
        }
    }
}
