namespace Assets
{
    public class Feld
    {
        public FeldTerrain Terrain { get; set; }
        public int Einheiten { get; set; }
        public double Wachstumsfaktor { get; set; }
    }

    public enum FeldTerrain
    {
        Wiese,
        Wasser,
        Wald,
        Gestein,
        Sand,
    }
}
