namespace AT_TourManager.Data.Models
{
    public class Destino
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public int PaisDestinoId { get; set; } 
        public PaisDestino PaisDestino { get; set; }
    }
}
