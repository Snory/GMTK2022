public class PlayerHealthEventData : EventData
{
    public int Health;

    public PlayerHealthEventData(int health)
    {
        Health = health;
    }
}