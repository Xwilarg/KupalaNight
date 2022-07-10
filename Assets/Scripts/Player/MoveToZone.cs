using UnityEngine;
using UnityWithUkraine.Story;

namespace UnityWithUkraine.Player
{
    public class MoveToZone : MonoBehaviour
    {
        [SerializeField]
        private int _newZone;

        [SerializeField]
        private bool _requireTorch;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (_requireTorch && !PlayerController.Instance.Contains(Item.ItemType.TorchOn))
            {
                PlayerController.Instance.Stop();
                StoryManager.Instance.ReadText("level2_mcblock");
            }
            else
            {
                PlayerController.Instance.MoveToZone(_newZone);
            }
        }
    }
}
