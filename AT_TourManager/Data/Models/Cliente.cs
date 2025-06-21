using System.ComponentModel.DataAnnotations;

namespace AT_TourManager.Data.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email valido")]
        [MaxLength(100, ErrorMessage = "O email não pode passar de 100 caracteres")]
        public string Email { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
