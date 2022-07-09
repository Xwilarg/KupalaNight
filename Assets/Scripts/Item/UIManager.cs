using TMPro;
using UnityEngine;

namespace UnityWithUkraine.Item
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _storyText;

        [SerializeField]
        private GameObject _storyContainer;

        public static UIManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public void DisplayText(string text)
        {
            _storyContainer.SetActive(true);
            _storyText.text = text;
        }

        public void HideStoryContainer()
        {
            _storyContainer.SetActive(false);
        }
    }
}
