﻿namespace AT_TourManager.Data.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataInicio { get; set; }
        public int CapacidadeMaxima { get; set; }
        public ICollection<Destino> Destinos { get; set; } = new List<Destino>();

        public delegate decimal CalculateDelegate(decimal preco);

        public event Action<PacoteTuristico> CapacityReached;

        public delegate bool VerificarDisponibilidadeDelegate(int capacidadeMaxima, int reservasAtuais);

        protected virtual void OnCapacityReached()
        {
            CapacityReached?.Invoke(this);
        }

        public bool VerificarCapacidade(int reservasAtuais)
        {
            if (reservasAtuais >= CapacidadeMaxima)
            {
                OnCapacityReached();
                return false;
            }
            return true;
        }


    }
}
