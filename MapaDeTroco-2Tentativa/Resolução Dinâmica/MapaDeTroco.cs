namespace MapaDeTroco
{
    public class TrocoMoeda
    {
        public double Moeda { get; set; }
        public int Quantidade { get; set; }

        public TrocoMoeda(double moeda, int quantidade)
        {
            Moeda = moeda;
            Quantidade = quantidade;
        }
    }
}
