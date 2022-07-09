using UnityEngine;
using UnityWithUkraine.Player;

namespace UnityWithUkraine.Item
{
    public class Bondfire : MonoBehaviour
    {
        private bool _isOn;

        private Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void Interact()
        {
            if (!_isOn)
            {
                if (PlayerController.Instance.Contains(ItemType.FlowerLightupFire))
                {
                    PlayerController.Instance.RemoveFromInventory(ItemType.FlowerLightupFire);
                    _isOn = true;
                    _anim.SetBool("IsOn", true);
                }
            }
            else
            {
                if (PlayerController.Instance.Contains(ItemType.TorchOff))
                {
                    PlayerController.Instance.RemoveFromInventory(ItemType.TorchOff);
                    PlayerController.Instance.AddToInventory(ItemType.TorchOn);
                }
            }
        }
    }
}
