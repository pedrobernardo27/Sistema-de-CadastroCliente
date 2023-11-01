using CadastroClienteRepository.Model;

namespace CadastroClienteRepository.Interface
{
    public interface ICadastroRepository
    {
        ValueTask Editar(ClienteModel cliente);
        ValueTask Excluir(ClienteModel cliente);
        ValueTask<EnderecoModel> Buscar(int id);
        ValueTask<IEnumerable<ClienteModel>> Listar();
        ValueTask<ClienteModel> BuscarClientePorId(int id);
        ValueTask CadatroNovoCliente(ClienteModel cliente);
    }
}
