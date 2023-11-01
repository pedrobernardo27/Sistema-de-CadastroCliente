using Microsoft.EntityFrameworkCore;
using CadastroClienteRepository.Model;

namespace CadastroCliente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optins) : base(optins)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
    }
}
