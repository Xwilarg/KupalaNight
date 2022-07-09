using UnityEngine;

namespace UnityWithUkraine.Item
{
    public class Tackable : MonoBehaviour
    {
        [SerializeField]
        private ItemType _item;

        public ItemType Item => _item;
    }
}
