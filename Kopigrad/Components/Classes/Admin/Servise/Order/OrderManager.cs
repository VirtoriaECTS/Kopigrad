using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;

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




        public List<Models.Contacttype> GetStatusType()
        {
            using (var context = new Models.KopigradContext())
            {
                return context.Contacttypes.ToList();
            }
        }


        public List<Classes.Data.DataOrdersClass> getOrderItems()
        {
            List<Classes.Data.DataOrdersClass> list = new List<Classes.Data.DataOrdersClass>();

            using (var context = new KopigradContext())
            {
                // Получаем заказы с включёнными навигационными свойствами
                var orders = context.Orders
                    .Include(o => o.IdTableMiniServiceNavigation)
                        .ThenInclude(t => t.IdMiniServiceNavigation)
                    .Include(o => o.IdTableMiniServiceNavigation.IdColumnNameNavigation)
                    .Include(o => o.IdTableMiniServiceNavigation.IdMaterialNavigation)
                    .Include(o => o.Orderitems)
                    .ToList();

                // Группируем по IdOrder (хотя в этом списке уже должны быть уникальные заказы)
                // Но для наглядности — так соберём пути файлов для каждого заказа
                foreach (var order in orders)
                {
                    int idOrder = order.IdOrder;

                    var table = order.IdTableMiniServiceNavigation;
                    var miniServiceItem = table?.IdMiniServiceNavigation;
                    var service = miniServiceItem != null
                        ? context.Services.FirstOrDefault(s => s.IdService == miniServiceItem.IdService)
                        : null;

                    string nameService = FormatName(service?.NameService);
                    string nameMiniService = FormatName(miniServiceItem?.NameMiniServise);
                    string column = FormatName(table?.IdColumnNameNavigation?.NameColumn);
                    string material = FormatName(table?.IdMaterialNavigation?.NameMaterial);

                    // Собираем уникальные пути файлов, чтобы убрать дубликаты
                    var filePaths = order.Orderitems
                        .Select(i => i.FilePath)
                        .Distinct()  // <--- убираем повторы
                        .ToList();

                    var data = new Classes.Data.DataOrdersClass(idOrder, nameService, nameMiniService, column, material, filePaths);
                    list.Add(data);
                }
            }

            return list;
        }

        private string FormatName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

    }
}
