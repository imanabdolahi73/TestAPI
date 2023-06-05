using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestApi.Contracts;
using TestApi.Models;

namespace TestApi.Repositories
{
    public class CustomerRepositiry : ICustomerRepository
    {
        private readonly TestApiDbContext _context;
        private IMemoryCache _cache;
        public CustomerRepositiry(TestApiDbContext context , IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customers.SingleAsync(a => a.CustomerId == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Find(int id)
        {
            var casheCustomer = _cache.Get<Customer>(id);
            if (casheCustomer != null)
            {
                return casheCustomer;
            }
            else
            {
                var customer = await _context.Customers.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
                var casheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(customer.CustomerId, customer, casheOption);
                return customer;
            }
            
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public int CountCustomer()
        {
            return _context.Customers.Count();
        }
    }
}
