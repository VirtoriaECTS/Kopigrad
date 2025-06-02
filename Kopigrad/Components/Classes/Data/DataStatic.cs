namespace Kopigrad.Components.Classes.Data
{
    public class DataStatic
    {
        public int count;
        public double data;
        public DateTime XAxisLabels;

        public DataStatic(int count, double data, DateTime xAxisLabels)
        {
            this.count = count;
            this.data = data;
            XAxisLabels = xAxisLabels;
        }
    }
}
