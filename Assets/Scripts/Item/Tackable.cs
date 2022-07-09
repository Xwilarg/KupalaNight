using UnityEngine;
using UnityWithUkraine.Player;

namespace UnityWithUkraine.Item
{
    public class Tackable : MonoBehaviour
    {
        [SerializeField]
        private ItemType _item;

        public ItemType Item => _item;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController.Instance.AddItemInRange(this);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController.Instance.RemoveItemInRange(this);
            }
        }
    }
}
