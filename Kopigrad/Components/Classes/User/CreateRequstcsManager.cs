using Kopigrad.Migrations;
using Kopigrad.Models;
using System.Collections.Generic;

namespace Kopigrad.Components.Classes.User
{
    public class CreateRequstcsManager
    {

        public int FirstIdTableMiniService(int idMiniService, int idMaterial, int idColimnName)
        {
            int id = 0;
            using (var context = new KopigradContext())
            {
                id = context.Tableminiservices.Where(x => x.IdMiniService == idMiniService).Where(x => x.IdMaterial == idMaterial).Where(x => x.IdColumnName == idColimnName).Select(x => x.IdTableMiniService).First();
            }
            return id;

        }

        public void CreateOrder(string name, int contactType, string contact, int tableMiniService, List<string> filePaths, decimal price, int count)
        {
            int id = AddOrderTable(name, contactType, contact, tableMiniService, count, price);
            AddOrderItemsTable(id, filePaths);
        }


        public int AddOrderTable(string name, int contactType, string contact, int idTable, int count, decimal price)
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
                    DataOrder = currentDate,
                    IdTableMiniService = idTable,
                    Count = count,
                    Price = price
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                return newOrder.IdOrder;
            }




        }



        public void AddOrderItemsTable(int order, List<string> filePaths)
        {

            using (var context = new KopigradContext())
            {

                foreach (var item in filePaths)
                {
                    var newOrderItem = new Orderitem()
                    {
                        IdOrder = order,
                        FilePath = item
                    };

                    context.Orderitems.Add(newOrderItem);

                }
                context.SaveChanges();
            }
        }

    }
     

    
}
