namespace YOSSAPONJ.FUELCOMPARISON.WEB.Models
{
    public class BangchakOilPriceModel
    {
        /*
        OilPriceID: number;
        Banner: string;
        OilDateNow: string; //Date as string
        OilPriceDate: string; //Date as string
        OilPriceTime: string; //Time as string
        OilMessageDate: string; //Date as string
        OilMessageTime: string; //Time as string
        ScreenType: number;
        OilRemark: string; //Remark message
        OilList: string; //JSON as string
        GasList: string;
         */

        public int OilPriceID { get; set; }
        public string Banner { get; set; }
        public string OilDateNow { get; set; }
        public string OilPriceDate { get; set; }
        public string OilPriceTime { get; set; }
        public string OilMessageDate { get; set; }
        public string OilMessageTime { get; set; }
        public int ScreenType { get; set; }
        public string OilRemark { get; set; }
        public string OilList { get; set; }
        public string GasList { get; set; }
    }
}
