namespace W_4_API_CORE_CRUD_REPO.Repository
{
    public interface Iproduct
    {

        Task<IEnumerable<Product>> Get();


        Task<Product> Get(int ProductId);

        Task<Product> Create(Product product);


        Task Update(Product product);


        Task Delete(int ProductId);
    }
}
