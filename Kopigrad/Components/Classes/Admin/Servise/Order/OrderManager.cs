using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Admin.Servise.Order
{
    public class OrderManager
    {
        public List<Models.Order> getOrder()
        {
            using(var context = new Models.KopigradContext())
            {
                return context.Orders.ToList();
            }
        } 


        public List<Models.Status> getStatus()
        {
            using (var context = new Models.KopigradContext())
            {
                return context.Statuses.ToList();
            }
        }

        public List<Models.Order> GetOrders()
        {
            using (var context = new Models.KopigradContext())
            {
                return context.Orders.ToList();
            }
        }




        public List<Models.ContactType> GetStatusType()
        {
            using (var context = new Models.KopigradContext())
            {
                return context.ContactTypes.ToList();
            }
        }


        public List<Classes.Data.DataOrdersClass> getOrderItems()
        {
            List<Classes.Data.DataOrdersClass> list = new List<Data.DataOrdersClass>();
            using (var context = new KopigradContext())
            {
                var orders = context.Orders.ToList();

                foreach (var item in orders)
                {
                    int idTable = item.IdTableMiniService;
                    var table = context.Tableminiservices.Where(x => x.IdTableMiniService == idTable).FirstOrDefault();
                    var miniServiceItem = context.Miniservices.Where(x => x.IdMiniService == table.IdMiniService).FirstOrDefault();
                    int idServiceTable = miniServiceItem.IdService;

                    int idOrder = item.IdOrder;
                    
                    string nameService = context.Services.Where(x => x.IdService == idServiceTable).Select(x => x.NameService).FirstOrDefault();
                    string nameminiService = context.Miniservices.Where(x => x.IdMiniService == miniServiceItem.IdMiniService).Select(x => x.NameMiniServise).FirstOrDefault();
                    string column = context.Columnnames.Where(x => x.IdColumnNames == table.IdColumnName).Select(x => x.NameColumn).FirstOrDefault();
                    string material = context.Materials.Where(x => x.IdMaterial == table.IdMaterial).Select(x => x.NameMaterial).FirstOrDefault();

                    nameService = string.IsNullOrWhiteSpace(nameService) ? nameService : char.ToUpper(nameService[0]) + nameService.Substring(1).ToLower();
                    nameminiService = string.IsNullOrWhiteSpace(nameminiService) ? nameminiService : char.ToUpper(nameminiService[0]) + nameminiService.Substring(1).ToLower();
                    column = string.IsNullOrWhiteSpace(column) ? column : char.ToUpper(column[0]) + column.Substring(1).ToLower();
                    material = string.IsNullOrWhiteSpace(material) ? material : char.ToUpper(material[0]) + material.Substring(1).ToLower();


                    List<string> filePaths = new List<string>();
                    var orderItems = context.Orderitems.Where(x => x.IdOrder == item.IdOrder).ToList();

                    foreach(var i in orderItems)
                    {
                        filePaths.Add(i.FilePath);
                    }

                    Classes.Data.DataOrdersClass data = new Data.DataOrdersClass(idOrder, nameService, nameminiService, column, material, filePaths);

                    list.Add(data);

                    filePaths.Clear();
                }
            }

            return list;
        }
    }
}
