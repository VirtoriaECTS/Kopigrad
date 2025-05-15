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
    }
}
