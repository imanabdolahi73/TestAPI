using TestApi.Models;

namespace TestApi.Contracts
{
    public interface ISalesPersonsRepository
    {
        IEnumerable<SalesPerson> GetAll();
        Task<SalesPerson> Add(SalesPerson sales);
        Task<SalesPerson> Find(int id);
        Task<SalesPerson> Update(SalesPerson sales);
        Task<SalesPerson> Remove(int id);
        Task<bool> IsExists(int id);
    }
}
