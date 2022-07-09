using UnityEngine;

namespace UnityWithUkraine.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        public float PlayerSpeed;
        public float DistanceBeforeStop;
    }
}