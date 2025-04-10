namespace Kopigrad.Components.Classes.Data
{
    public class InfoMiniServiceData
    {
        public int idMiniService;
        public string nameMiniService = "";
        public string topCategory = "";
        public string bottomCategory = "";

        public int trSum; //строки
        public int tdSum; //столбцы


        public List<string> headerValues = new List<string>();
        public List<int> materialIds = new List<int>();
        public List<List<decimal>> priceList = new List<List<decimal>>();


        public InfoMiniServiceData(int idMiniService, string nameMiniService, string topCategory, string bottomCategory,  List<string> headerValues, List<int> materialIds)
        {
            this.idMiniService = idMiniService;
            this.nameMiniService = nameMiniService;
            this.topCategory = topCategory;
            this.bottomCategory = bottomCategory;
            this.headerValues = headerValues;
            this.materialIds = materialIds;

        }
    }
}
