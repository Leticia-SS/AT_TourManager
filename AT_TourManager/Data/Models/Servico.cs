﻿namespace AT_TourManager.Data.Models
{
    public class Servico
    {
        public static Func<int, int, decimal> CalcularValorTotal = (diarias, valorDiaria) => diarias * valorDiaria;

        public static decimal CalcularPrecoComDesconto(decimal preco)
        {
            return preco * 0.9m;
        }

        public static bool VerificarDisponibilidade(int capacidadeMaxima, int reservasAtuais)
        {
            return reservasAtuais < capacidadeMaxima;
        }

        public static bool ValidarDataFutura(DateTime dataServico)
        {
            return dataServico > DateTime.Now;
        }

    }
}
