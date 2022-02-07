using System;

namespace Strategy.Common
{
    [Serializable]
    public struct Rotation
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Rotation(float X, float Y, float Z, float W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
    }
}
