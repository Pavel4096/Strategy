using Strategy.CommonTypes;
using System;
using System.Collections.Generic;

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
        public float ProductionTime;
        public string Icon;
        public List<CommandTypes> Commands;
        public List<ItemTypes> Items;
    }
}
