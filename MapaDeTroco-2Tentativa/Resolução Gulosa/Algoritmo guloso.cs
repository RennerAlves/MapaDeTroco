using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaDeTroco_2Tentativa.Resolução_Gulosa
{
    internal class Algoritmo_guloso
   
    {
        class Program
        {
            static void Main()
            {
                List<double> moedas = new List<double> { 0.05, 0.10, 0.25, 0.50, 1, 2, 5, 10, 20, 50, 100, 200 };
                Console.WriteLine("Entre com o valor a calcular o mapa de troco: ");
                string valorInput = Console.ReadLine();

                // Tente converter para double
                if (double.TryParse(valorInput, out double valorDouble))
                {
                    List<TrocoMoeda> troco = CalcularTroco(moedas, valorDouble);
                    Console.WriteLine("O troco é: ");
                    foreach (var trocoMoeda in troco)
                    {
                        Console.WriteLine($"{trocoMoeda.Quantidade} notas de {trocoMoeda.Moeda}");
                    }
                }
                // Caso não seja possível converter para double, tenta converter para int
                else if (int.TryParse(valorInput, out int valorInt))
                {
                    List<TrocoMoeda> troco = CalcularTroco(moedas, valorInt);
                    Console.WriteLine("O troco é: ");
                    foreach (var trocoMoeda in troco)
                    {
                        Console.WriteLine($"{trocoMoeda.Quantidade} moedas de {trocoMoeda.Moeda}");
                    }
                }
                else
                {
                    Console.WriteLine("Valor inválido.");
                }
            }

            static List<TrocoMoeda> CalcularTroco(List<double> moedas, double valor)
            {
                List<TrocoMoeda> troco = new List<TrocoMoeda>();
                for (int i = moedas.Count - 1; i >= 0; i--)
                {
                    double moeda = moedas[i];
                    int quantidade = (int)(valor / moeda);
                    if (quantidade > 0)
                    {
                        troco.Add(new TrocoMoeda(moeda, quantidade));
                        valor -= moeda * quantidade;
                    }
                }
                return troco;
            }
        }

        public class TrocoMoeda
        {
            public double Moeda { get; }
            public int Quantidade { get; }

            public TrocoMoeda(double moeda, int quantidade)
            {
                Moeda = moeda;
                Quantidade = quantidade;
            }
        }
    }
}
