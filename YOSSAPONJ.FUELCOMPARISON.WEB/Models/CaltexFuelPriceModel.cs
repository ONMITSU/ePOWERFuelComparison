namespace YOSSAPONJ.FUELCOMPARISON.WEB.Models
{
    public class CaltexFuelPriceModel
    {
        public DateTime LastUpdate { get; set; }
        public string LastUpdateText { get; set; }
        public List<CaltexFuelListModel> Fuels { get; set;}
        /// <summary>
        /// Log by local application - last data fetch date.
        /// </summary>
        public DateTime LastFetched { get; set; }
    }
}
