using UnityEngine;

namespace UnityWithUkraine.Item
{
    public class Takable : MonoBehaviour
    {
        public bool DeleteOnInteraction;

        public ItemType Item;
        public ItemType Requirement;

        public string VNToken;

        public bool IsBondfire;
    }
}
