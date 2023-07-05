namespace Battleships.App.Configuration;

public class GridConfiguration
{
    public const string ConfigurationSection = "Grid";

    public int Width { get; set; }
    public int Height { get; set; }
    public List<Ship> ShipsToSpawn { get; set; }

    public class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}
