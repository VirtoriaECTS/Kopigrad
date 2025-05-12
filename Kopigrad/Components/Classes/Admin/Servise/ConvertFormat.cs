using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ConvertPdfonPrice
    {

        public string GetPaperFormat(double width, double height)
        {
            // Преобразуем размеры в миллиметры (1 pt = 0.352778 mm)
            var w = Math.Round(width * 0.352778);
            var h = Math.Round(height * 0.352778);

            // Учитываем ориентацию (портретная)
            if (w > h)
            {
                var temp = w;
                w = h;
                h = temp;
            }

            // ISO 216 размеры (в мм)
            if (Math.Abs(w - 841) <= 5 && Math.Abs(h - 1189) <= 5) return "A0";
            if (Math.Abs(w - 594) <= 5 && Math.Abs(h - 841) <= 5) return "A1";
            if (Math.Abs(w - 420) <= 5 && Math.Abs(h - 594) <= 5) return "A2";
            if (Math.Abs(w - 297) <= 5 && Math.Abs(h - 420) <= 5) return "A3";
            if (Math.Abs(w - 210) <= 5 && Math.Abs(h - 297) <= 5) return "A4";

            return $"{w} x {h} ";
        }


        public decimal getPriceNormal(int idMiniService, int idColumnName, int idMaterial, string text,double width, double height)
        {
            if(text.Length != 2)
            {
                using (var context = new KopigradContext())
                {
                    decimal priceMetr = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(x => x.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColumnName).Select(x => x.Price).First();

                    decimal metrPog = (decimal)(height / 1000);

                    decimal metr = Math.Round(metrPog, 1);

                    decimal price = priceMetr * metr;

                    return price;

                }
            }
            else
            {
                using (var context = new KopigradContext())
                {
                    decimal price = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(x => x.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColumnName).Select(x => x.Price).First();
                    return price;
                }
            }
           
        }








    }
}
