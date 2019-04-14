using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PizzaStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Order
                           .Include(o => o.ProductOrder)
                           .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.ProductOrder.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Order.Add(order);
            }
            context.SaveChanges();
        }
    }
}
