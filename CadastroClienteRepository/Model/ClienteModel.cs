namespace CadastroClienteRepository.Model
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public ICollection<EnderecoModel> Endereco { get; set; }
    }
}
