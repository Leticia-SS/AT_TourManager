using System.ComponentModel.DataAnnotations;

namespace AT_TourManager.Data.Models
{
    public class PaisDestino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do país é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres")]
        [Display(Name = "Nome do País")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A sigla é obrigatória")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "A sigla deve ter entre 2 e 3 caracterse")]
        public string Sigla { get; set; }
        public ICollection<Destino> Destinos { get; set; } = new List<Destino>();
    }
}
