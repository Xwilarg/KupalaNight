using UnityEngine;
using UnityWithUkraine.Player;

namespace UnityWithUkraine.Item
{
    public class Tackable : MonoBehaviour
    {
        [SerializeField]
        private ItemType _item;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController.Instance.AddItem(this);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController.Instance.RemoveItem(this);
            }
        }
    }
}
