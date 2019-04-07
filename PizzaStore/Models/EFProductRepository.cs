using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Product> Products => _context.Products;
    }
}
