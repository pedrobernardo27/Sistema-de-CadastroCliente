namespace CadastroClienteRepository.Model
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public ClienteModel Cliente { get; set; }
    }
}
