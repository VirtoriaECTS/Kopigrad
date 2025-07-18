using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise
{
    public class ManagerOrders
    {

        public string ChangeStatusOrder(int orderId, int idStatus)
        {
            using (var context = new KopigradContext())
            {
                var order = context.Orders.Where(x => x.IdOrder == orderId).FirstOrDefault();
                order.IdStatus = idStatus;
                string anwer = "";

                if(idStatus == 1)
                {
                    anwer = changeCountMaterial(order);
                }

                if(anwer.Length == 0)
                {
                    context.SaveChanges();
                    return "";
                }
                else
                {
                    return anwer;
                }
            }
        }


        public string changeCountMaterial(Models.Order order) { 

            using(var context = new KopigradContext())
            {
                var tableminiService = context.Tableminiservices.Where(x => x.IdTableMiniService == order.IdTableMiniService).FirstOrDefault();
                var material = context.Materials.Where(x => x.IdMaterial == tableminiService.IdMaterial).FirstOrDefault();

                context.SaveChanges();
                return "";
            }
        }
    }
}
