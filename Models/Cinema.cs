using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório.")]
        public string NomeCinema { get; set; }

        //possui a relacao de 1 pra 1
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

    }
}
