using System;

namespace Strategy.Common
{
    [Serializable]
    public sealed class ItemData
    {
        public string Name;
        public string Prefab;
        public ItemTypes Id;
        public string StringId;
        public float MaxHP;
        public float Attack;
        public string Icon;
        public CommandTypes[] Commands;
        public ItemTypes[] Items;
    }
}
