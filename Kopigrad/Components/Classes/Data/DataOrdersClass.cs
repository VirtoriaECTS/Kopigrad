namespace Kopigrad.Components.Classes.Data
{
    public class DataOrdersClass
    {
        public DataOrdersClass(int idOrderItems, string nameService, string miniService, string nameColumn, string nameMaterial, List<string> dataList)
        {
            IdOrderItems = idOrderItems;
            this.nameService = nameService;
            this.miniService = miniService;
            this.nameColumn = nameColumn;
            this.nameMaterial = nameMaterial;
            this.dataList = dataList;
        }

        public int IdOrderItems { get; set; }
        public string nameService { get; set; }
        public string miniService { get; set; }
        public string nameColumn { get; set; }
        public string nameMaterial { get; set; }
        public List<string> dataList { get; set; }


    }
}
