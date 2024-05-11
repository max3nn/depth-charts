namespace Domain.Common.Players;
public class Player
{
    public Player(string name, int number)
    {
        Name = name;
        Number = number;
    }

    public int Number { get; private set; }
    public string Name { get; private set; }
}
