using System;

namespace Strategy.Common
{
    [Serializable]
    public struct Position
    {
        public float x;
        public float y;
        public float z;

        public Position(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
}
