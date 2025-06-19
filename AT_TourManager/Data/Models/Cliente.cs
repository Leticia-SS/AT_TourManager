namespace AT_TourManager.Data.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
