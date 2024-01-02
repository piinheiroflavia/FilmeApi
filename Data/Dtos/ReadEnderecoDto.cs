using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Data.Dtos
{
    public class ReadEnderecoDto
    {
        public int Id { get; set; }
        
        public string Logradouro { get; set; }
    
        public int Numero { get; set; }

        public string Endereco { get; set; }
    }
}
