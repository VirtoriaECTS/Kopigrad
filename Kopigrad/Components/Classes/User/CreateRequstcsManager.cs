using Kopigrad.Migrations;
using Kopigrad.Models;

namespace Kopigrad.Components.Classes.User
{
    public class CreateRequstcsManager
    {

        public int FirstIdTableMiniService(int idMiniService, int idMaterial, int idColimnName)
        {
            int id = 0;
            using(var context = new KopigradContext())
            {
               id =  context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(x => x.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColimnName).Select(x => x.IdTableMiniService).First();
            }
            return id;
    
        }
        
        public void CreateOrder(string name, int contactType, string contact, int tableMiniService, List<string> filePaths, decimal price, int count)
        {
            int id = AddOrderTable(name, contactType, contact);
            AddOrderItemsTable(id, tableMiniService, filePaths, price, count);
        }

        public int AddOrderTable(string name, int contactType, string contact)
        {
            DateTime currentDate = DateTime.Now;

            using (var context = new KopigradContext())
            {
                var newOrder = new Order()
                {
                    NameUser = name,
                    ContactTypeId = contactType,
                    Contact = contact,
                    IdStatus = 3,
                    DataOrder = currentDate
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                return newOrder.IdOrder;  
            }

            


        }

        public void AddOrderItemsTable(int order, int tableMiniService, List<string> filePaths, decimal price, int count)
        {
            string text = string.Join(";", filePaths);

            using (var context = new KopigradContext())
            {
                var newOrder = new Orderitem()
                {
                    IdOrder = order,
                    IdTableMiniService = tableMiniService,
                    FilePath = text,
                    Price = price,
                    Count = count


                };


                context.Orderitems.Add(newOrder);
                context.SaveChanges();
            }
        }

    }
}
