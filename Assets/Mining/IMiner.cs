
public class Ore {
    public string Name;

    public override string ToString()
    {
        return Name;
    }
}

public interface IMiner
{
    Inventory<Ore> GetInventory();
}