using CadastroClienteService.Model;
using CadastroClienteRepository.Model;

namespace CadastroClienteServices.Service
{ 
    public interface ICadastroServices
    {
        ValueTask<EnderecoModel> BuscarEnderecoCliente(int id);
        ValueTask<ClienteEnderecoViewModel> BuscarCliente(int id);
        ValueTask<IEnumerable<ClienteEnderecoViewModel>> ListarCliente();
        ValueTask<IEnumerable<ClienteEnderecoViewModel>> EditarCliente(ClienteEnderecoViewModel cliente);
        ValueTask<IEnumerable<ClienteEnderecoViewModel>> ExcluirCliente(int id);
        ValueTask<IEnumerable<ClienteEnderecoViewModel>> CadastrarCliente(ClienteEnderecoViewModel cliente);
    }
}

