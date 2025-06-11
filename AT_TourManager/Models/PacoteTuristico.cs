namespace AT_TourManager.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInicio { get; set; }
        public int CapacidadeMaxima { get; set; }
        public List<Destino> Destinos { get; set; } = new List<Destino>();
        public delegate decimal CalculateDelegate(decimal preco);

    }
}
