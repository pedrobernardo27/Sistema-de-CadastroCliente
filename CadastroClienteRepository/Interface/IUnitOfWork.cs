namespace CadastroClienteRepository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTrasaction();
        Task Commit();
        Task Rollback();
    }
}
