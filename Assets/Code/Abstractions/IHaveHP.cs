namespace Strategy.Abstractions
{
    public interface IHaveHP
    {
        float HP { get; }
        void DoDamage(float damage);
    }
}
