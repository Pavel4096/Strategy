using System;

namespace Strategy.Common
{
    [Serializable]
    public sealed class EnvironmentItem
    {
        public string Prefab;
        public Position Position;
        public Rotation Rotation;

        public EnvironmentItem(string prefab, Position position, Rotation rotation)
        {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }
    }
}
