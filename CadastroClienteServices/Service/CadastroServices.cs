using CadastroClienteService.Model;
using CadastroClienteRepository.Model;
using CadastroClienteRepository.Interface;

namespace CadastroClienteServices.Service
{
    public class CadastroServices : ICadastroServices
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CadastroServices(ICadastroRepository cadastroRepository, IUnitOfWork unitOfWork)
        {
            _cadastroRepository = cadastroRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<IEnumerable<ClienteEnderecoViewModel>> CadastrarCliente(ClienteEnderecoViewModel cliente)
        {
            try
            {
                var resultCliente = new List<ClienteEnderecoViewModel>();
                using (_unitOfWork.BeginTrasaction())
                {
                    var clienteMapeado = new ClienteModel
                    {
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Idade = cliente.Idade,
                        Endereco = new List<EnderecoModel>()
                    };

                    if (cliente.Rua != null && cliente.Bairro != null && cliente.Cidade != null)
                    {
                        var novoEndereco = new EnderecoModel
                        {
                            Rua = cliente.Rua,
                            Bairro = cliente.Bairro,
                            Cidade = cliente.Cidade
                        };

                        clienteMapeado.Endereco.Add(novoEndereco);
                    }

                    await _cadastroRepository.CadatroNovoCliente(clienteMapeado);
                    resultCliente = (List<ClienteEnderecoViewModel>)await ListarCliente();
                    await _unitOfWork.Commit();
                }                

                return resultCliente;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new Exception($"Erro ao efetuar CadastrarCliente. {ex.Message}");

            }
            
        }

        public async ValueTask<IEnumerable<ClienteEnderecoViewModel>> ListarCliente()
        {
            var lstClientes = new List<ClienteEnderecoViewModel>();
            var resultClientes = await _cadastroRepository.Listar();

            if (resultClientes.Any())
            {
                foreach (var cliente in resultClientes)
                {
                    var novoCliente = new ClienteEnderecoViewModel
                    {
                        Id = cliente.Id,
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Idade = cliente.Idade
                    };

                    lstClientes.Add(novoCliente);
                }
            }

            return lstClientes;
        }

        public async ValueTask<EnderecoModel> BuscarEnderecoCliente(int id)
        {
            var endereco = new EnderecoModel();
            var enderecoBuscado = await _cadastroRepository.Buscar(id);

            if (enderecoBuscado != null)
            {
                endereco = enderecoBuscado;
            }

            return endereco;
        }

        public async ValueTask<ClienteEnderecoViewModel> BuscarCliente(int id)
        {
            var clienteMapeado = new ClienteEnderecoViewModel();
            var clienteEncontrado = await _cadastroRepository.BuscarClientePorId(id);

            if (clienteEncontrado != null)
            {
                clienteMapeado.Nome = clienteEncontrado.Nome;
                clienteMapeado.Sobrenome = clienteEncontrado.Sobrenome;
                clienteMapeado.Idade = clienteEncontrado.Idade;

                if (clienteEncontrado.Endereco != null)
                {
                    foreach (var endereco in clienteEncontrado.Endereco)
                    {
                        clienteMapeado.Rua = endereco.Rua;
                        clienteMapeado.Cidade = endereco.Cidade;
                        clienteMapeado.Bairro = endereco.Bairro;
                    }
                }
            }

            return clienteMapeado;
        }

        public async ValueTask<IEnumerable<ClienteEnderecoViewModel>> EditarCliente(ClienteEnderecoViewModel cliente)
        {
            try
            {
                var clienteMapeado = new ClienteModel
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Idade = cliente.Idade,
                    Endereco = new List<EnderecoModel>()
                };

                if (cliente.Rua != null && cliente.Bairro != null && cliente.Cidade != null)
                {
                    var novoEndereco = new EnderecoModel
                    {
                        Id = cliente.Id,
                        Rua = cliente.Rua,
                        Bairro = cliente.Bairro,
                        Cidade = cliente.Cidade
                    };

                    clienteMapeado.Endereco.Add(novoEndereco);
                }

                await _cadastroRepository.Editar(clienteMapeado);
                var ResultCliente = await ListarCliente();
                return ResultCliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao efetuar EditarCliente. {ex.Message}");
            }
        }

        public async ValueTask<IEnumerable<ClienteEnderecoViewModel>> ExcluirCliente(int id)
        {
            try
            {
                var clienteResult = await _cadastroRepository.BuscarClientePorId(id);

                await _cadastroRepository.Excluir(clienteResult);
                var ResultCliente = await ListarCliente();
                return ResultCliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao efetuar ExcluirCliente. {ex.Message}");
            }
        }
    }
}
