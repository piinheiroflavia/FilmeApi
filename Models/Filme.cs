using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = " Campo Obrigatório ")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = " Campo Obrigatório ")]
        [MaxLength(50, ErrorMessage = "O tamanho do genero não pode exceder 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = " Campo Obrigatório ")]
        [Range(70, 600, ErrorMessage = "A duração deve de ser de entre 70 e 600 minutos")]
        public int Duracao { get; set;}

        [Required(ErrorMessage = " Campo Obrigatório ")]
        public string Diretor { get; set; }


        //criando uma lista com as sessoes de filmes:
        public virtual ICollection<Sessao> Sessaos { get; set; }

    }
}
