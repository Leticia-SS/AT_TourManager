using System.ComponentModel.DataAnnotations;

namespace AT_TourManager.Data.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        
        [Required]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int PacoteTuristicoId { get; set; }
        public PacoteTuristico PacoteTuristico { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public int NumeroDiarias { get; set; } 
        public decimal ValorTotal { get; set; }

        public delegate bool ValidarDataReservadaDelegate(DateTime dataReserva);

        public void CalcularValorTotal(Func<int, decimal, decimal> calculadora)
        {
            if (PacoteTuristico != null)
            {
                ValorTotal = calculadora(NumeroDiarias, PacoteTuristico.Preco);
            }
        }

    }
}
