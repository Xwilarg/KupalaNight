using UnityEngine;

namespace UnityWithUkraine.Player
{
    public class MoveToZone : MonoBehaviour
    {
        [SerializeField]
        private int _newZone;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController.Instance.MoveToZone(_newZone);
        }
    }
}
