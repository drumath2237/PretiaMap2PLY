using System;

namespace PretiaMapConverter
{
    [Serializable]
    public class MapData
    {
        public PosData[] points;
    }

    [Serializable]
    public class PosData
    {
        public float[] pos;
    }
}