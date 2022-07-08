using UnityEngine;

namespace UnityWithUkraine.Item
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        private GameObject _pressToTake;

        public void TogglePressToTake(bool value)
        {
            _pressToTake.SetActive(value);
        }
    }
}
