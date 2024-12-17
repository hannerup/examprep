namespace testasp.net_core.Models
{
    public class Vare
    {
        public int pris {  get; set; }
        public string ret { get; set; } = String.Empty;

        public int GetPris(int alder)
        {
            int prisAtBetale = pris;
            
            if (alder < 12) // Hvis rolling
            {
                prisAtBetale = prisAtBetale / 2;
            }
            else if (alder >= 65) // Eller olding
            {
                prisAtBetale = (int)((float)prisAtBetale * (float)0.80);
            }

            return prisAtBetale;
        }

    }


}
