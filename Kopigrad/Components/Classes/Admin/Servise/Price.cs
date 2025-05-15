using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class Price
    {
        public decimal getPrice(int idMiniService, int idColumnName, int idMaterial, int count)
        {
            using (var context = new KopigradContext())
            {
                decimal priceOne = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(x => x.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColumnName).Select(x => x.Price).First();
                decimal price = priceOne * count;
                
                return price;
            }

        }
    }
}
