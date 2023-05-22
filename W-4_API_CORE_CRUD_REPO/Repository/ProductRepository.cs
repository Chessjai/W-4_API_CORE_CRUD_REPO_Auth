using Microsoft.EntityFrameworkCore;

namespace W_4_API_CORE_CRUD_REPO.Repository
{
    public class ProductRepository:Iproduct
    {
        public readonly CoreDbcontext _context;

        public ProductRepository(CoreDbcontext context)

        {

            _context = context;

        }

        public async Task<Product> Create(Product product)

        {


            _context.products.Add(product);

            await _context.SaveChangesAsync();

            return product;

        }

        public async Task Delete(int ProductId)

        {


            var productToDelete = await _context.products.FindAsync(ProductId);


            _context.products.Remove(productToDelete);

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> Get()

        {


            return await _context.products.ToListAsync();

        }

        public async Task<Product> Get(int ProductId)

        {


            return await _context.products.FindAsync(ProductId);

        }

        public async Task Update(Product product)

        {

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

    }
}
