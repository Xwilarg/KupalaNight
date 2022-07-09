using System;
using UnityEngine;

namespace UnityWithUkraine.Player
{
    [Serializable]
    public class LevelData
    {
        public Transform[] PosInfo;
        public Transform ReferenceLeft, ReferenceRight;
        public int Min, Max;
    }
}
