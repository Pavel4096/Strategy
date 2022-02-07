namespace Strategy.Common
{
    public interface ILevelLoader
    {
        void LoadLevel(int index);
        void AddEnvironmentItemLoader(IEnvironmentItemLoader loader);
        void AddItemLoader(IItemLoader loader);
    }
}
