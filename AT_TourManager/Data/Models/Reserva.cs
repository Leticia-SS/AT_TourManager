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

        public delegate bool ValidarDataReservadaDelegate(DateTime dataReserva);



    }
}
