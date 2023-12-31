﻿using System.Globalization;
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
                    string inputStr = Console.ReadLine()!;
                    input = decimal.Parse(inputStr, NumberStyles.Float, CultureInfo.InvariantCulture);

                    if (input > 0)
                        break; 
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
            ExibirTroco(trocoInteiro, "nota");
            ExibirTroco(trocoDecimal, "moeda");


        }

        static List<TrocoMoeda> CalcularTrocoDinamicoNotas(List<double> moedas, int valor)
        {
            int[] dp = new int[valor + 1];
            List<TrocoMoeda> troco = new List<TrocoMoeda>();

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


                if (dp[dp.Length - 1] != 0)
                {


                    int valorAlvo = valor;
                    int numeroNotas = dp[dp.Length - 1];


                    for (int j = 0; j < moedas.Count; j++)
                    {
                        int moedaAtual = (int)moedas[j];
                        int quantidade = Math.Min(numeroNotas, valorAlvo / moedaAtual);

                        if (quantidade > 0)
                        {
                            troco.Add(new TrocoMoeda(moedaAtual, quantidade));
                        }

                        valorAlvo -= quantidade * moedaAtual;

                        if (valorAlvo == 0)
                        {
                            break;
                        }

                    }
                }
            }

            return troco;
        }



        static List<TrocoMoeda> CalcularTrocoDinamicoMoedas(List<double> moedas, decimal valor)
        {
            int valorCentavos = (int)(valor * 100);
            int[] dp = new int[valorCentavos + 1];
            List<TrocoMoeda> troco = new List<TrocoMoeda>();

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

                if (dp[dp.Length - 1] != 0)
                {
                    int valorAlvo = i;
                    int numeroMoedas = dp[dp.Length - 1];

                    for (int j = 0; j < moedas.Count; j++)
                    {
                        int moedaAtual = (int)(moedas[j] * 100);
                        int quantidade = Math.Min(numeroMoedas, valorAlvo / moedaAtual);

                        if (quantidade > 0)
                        {
                            troco.Add(new TrocoMoeda(moedaAtual / 100.0, quantidade));
                        }

                        valorAlvo -= quantidade * moedaAtual;

                        if (valorAlvo == 0)
                        {
                            break;
                        }
                    }
                }
            }

            return troco;
        }


static void ExibirTroco(List<TrocoMoeda> troco, string tipo)
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