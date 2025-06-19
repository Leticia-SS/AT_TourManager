namespace AT_TourManager.Data.Models
{
    public class PaisDestino
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Destino> Destinos { get; set; } = new List<Destino>();
    }
}
