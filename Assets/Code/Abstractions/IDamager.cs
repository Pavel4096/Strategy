namespace Strategy.Abstractions
{
    public interface IDamager
    {
        float Damage { get; }
        float AttackDistance { get; }
        int AttackDelay { get; }
    }
}
