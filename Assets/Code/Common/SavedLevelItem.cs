using Strategy.CommonTypes;
using System;

namespace Strategy.Common
{
    [Serializable]
    public sealed class SavedLevelItem
    {
        public ItemTypes Id;
        public string Prefab;
        public int Team;
        public Position Position;
        public Rotation Rotation;

        public SavedLevelItem(ItemTypes id, string prefab, int team, Position position, Rotation rotation)
        {
            Id = id;
            Prefab = prefab;
            Team = team;
            Position = position;
            Rotation = rotation;
        }
    }
}
