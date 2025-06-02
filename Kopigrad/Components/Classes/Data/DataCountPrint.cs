namespace Kopigrad.Components.Classes.Data
{
    public class DataCountPrint
    {

        public string data;
        public double[] kyocera;
        public double[] canon;
        public double[] canonTM;
        public double[] xerox;
        public double[] riso;

        public DataCountPrint(string data, double[] kyocera, double[] canon, double[] canonTM, double[] xerox, double[] riso)
        {
            this.data = data;
            this.kyocera = kyocera;
            this.canon = canon;
            this.canonTM = canonTM;
            this.xerox = xerox;
            this.riso = riso;
        }
    }
}
