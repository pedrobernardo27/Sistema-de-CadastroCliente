using CadastroCliente.Data;
using Microsoft.EntityFrameworkCore;
using CadastroClienteRepository.Model;
using CadastroClienteRepository.Interface;

namespace CadastroClienteRepository.Repository
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly ApplicationDbContext _db;

        public CadastroRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async ValueTask CadatroNovoCliente(ClienteModel cliente)
        {
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
        }

        public async ValueTask<IEnumerable<ClienteModel>> Listar()
        {
            IEnumerable<ClienteModel> cliente = _db.Clientes;
            return cliente;
        }

        public async ValueTask<EnderecoModel> Buscar(int id)
        {
            var endereco = await _db.Enderecos.FirstOrDefaultAsync(x => x.Cliente.Id == id);
            return endereco;
        }

        public async ValueTask<ClienteModel> BuscarClientePorId(int id)
        {
            var cliente = await _db.Clientes.Include(b => b.Endereco)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return cliente;
        }

        public async ValueTask Editar(ClienteModel cliente)
        {
            _db.Clientes.Update(cliente);
            _db.Enderecos.UpdateRange(cliente.Endereco);
            //_db.Entry(cliente.Endereco.FirstOrDefault()).State = EntityState.Modified;
            //_db.Entry(cliente).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async ValueTask Excluir(ClienteModel cliente)
        {
            try
            {
                _db.Clientes.Remove(cliente);
                _db.Enderecos.RemoveRange(cliente.Endereco);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
