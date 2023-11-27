using System;
using System.Collections.Generic;
using System.Globalization;
using MapaDeTroco;

namespace TrocoApp
{
    class Program
    {
        static void Main()
        {
            List<double> moedasInteiras = new List<double> { 100, 50, 20, 10, 5, 2, 1 };
            List<double> moedasDecimais = new List<double> { 0.50, 0.25, 0.10, 0.05 };
            

            decimal input = 0;

            while (true)
            {
                Console.WriteLine("Entre com o valor a calcular o mapa de troco: ");

                try
                {
                    input = Convert.ToDecimal(Console.ReadLine(), CultureInfo.InvariantCulture);

                    if (input > 0)
                        break; // Sai do loop se um valor válido for inserido
                    else
                        Console.WriteLine("Valor inválido. Certifique-se de inserir um número válido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao converter o valor. Certifique-se de inserir um número válido.");
                }
            }

            int parteInteira = (int)input;
            decimal parteDecimal = input - parteInteira;

            List<TrocoMoeda> trocoInteiro = CalcularTrocoDinamicoNotas(moedasInteiras, parteInteira);
            List<TrocoMoeda> trocoDecimal = CalcularTrocoDinamicoMoedas(moedasDecimais, parteDecimal);

            //List<TrocoMoeda> trocoTotal = new List<TrocoMoeda>();
            //trocoTotal.AddRange(trocoInteiro);
            //trocoTotal.AddRange(trocoDecimal);


            MostrarTroco(trocoInteiro, "nota");
            MostrarTroco(trocoDecimal, "moeda");


        }

        static List<TrocoMoeda> CalcularTrocoDinamicoNotas(List<double> moedas, int valor)
        {
            int[] dp = new int[valor + 1];

            for (int i = 1; i <= valor; i++)
            {
                dp[i] = int.MaxValue;
                foreach (var moeda in moedas)
                {
                    int moedaInt = (int)moeda;
                    if (i >= moedaInt)
                    {
                        dp[i] = Math.Min(dp[i], 1 + dp[i - moedaInt]);
                    }
                }
            }

            List<TrocoMoeda> troco = ConstruirTroco(moedas, dp);
            return troco;
        }

        static List<TrocoMoeda> CalcularTrocoDinamicoMoedas(List<double> moedas, decimal valor)
        {
            int valorCentavos = (int)(valor * 100);
            int[] dp = new int[valorCentavos + 1];

            for (int i = 1; i <= valorCentavos; i++)
            {
                dp[i] = int.MaxValue;
                foreach (var moeda in moedas)
                {
                    int moedaCentavos = (int)(moeda * 100);
                    if (i >= moedaCentavos)
                    {
                        dp[i] = Math.Min(dp[i], 1 + dp[i - moedaCentavos]);
                    }
                }
            }

            List<TrocoMoeda> troco = ConstruirTroco(moedas, dp);
            return troco;
        }

        static List<TrocoMoeda> ConstruirTroco(List<double> moedas, int[] dp)
        {
            List<TrocoMoeda> troco = new List<TrocoMoeda>();
            int index = dp.Length - 1;

            while (index > 0)
            {
                foreach (var moeda in moedas)
                {
                    int moedaInt = (int)moeda;
                    int moedaCentavos = (int)(moeda * 100);

                    if (moedaInt > 0 && index >= moedaInt && dp[index] == 1 + dp[index - moedaInt])
                    {
                        int quantidade = 0;
                        while (moedaInt > 0 && index >= moedaInt && dp[index] == 1 + dp[index - moedaInt])
                        {
                            quantidade++;
                            index -= moedaInt;
                        }

                        
                        troco.Add(new TrocoMoeda(moedaInt, quantidade));
                        break;
                    }
                    else if (moedaCentavos > 0 && index >= moedaCentavos && dp[index] == 1 + dp[index - moedaCentavos])
                    {
                        int quantidade = 0;
                        while (moedaCentavos > 0 && index >= moedaCentavos && dp[index] == 1 + dp[index - moedaCentavos])
                        {
                            quantidade++;
                            index -= moedaCentavos;
                        }

                        troco.Add(new TrocoMoeda(moeda, quantidade));
                        break;
                    }
                }
            }

            return troco;
        }

        static void MostrarTroco(List<TrocoMoeda> troco, string tipo)
        {
            Console.WriteLine($"O troco em {tipo}s é: ");
            foreach (var trocoMoeda in troco)
            {
                if (tipo == "moeda")
                {
                    int valorMoeda = (int)(trocoMoeda.Moeda * 100);
                    Console.WriteLine($"{trocoMoeda.Quantidade} {tipo}s de {valorMoeda}");
                }
                else
                {
                    Console.WriteLine($"{trocoMoeda.Quantidade} {tipo}s de {trocoMoeda.Moeda}");
                }
            }
        }

        
    }

}