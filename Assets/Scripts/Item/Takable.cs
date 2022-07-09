using UnityEngine;

namespace UnityWithUkraine.Item
{
    public class Takable : MonoBehaviour
    {
        [SerializeField]
        private ItemType _item;

        public ItemType Item => _item;
    }
}
