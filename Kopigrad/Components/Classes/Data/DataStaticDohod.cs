namespace Kopigrad.Components.Classes.Data
{
    public class DataStaticDohod
    {
        public string nameMonth;
        public List<double> dohod;

        public DataStaticDohod(string nameMonth, List<double> dohod)
        {
            this.nameMonth = nameMonth;
            this.dohod = dohod;
        }
    }
}
