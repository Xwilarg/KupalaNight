using UnityEngine;
using UnityWithUkraine.Item;
using UnityWithUkraine.Translation;

namespace UnityWithUkraine.Story
{
    public class StoryManager : MonoBehaviour
    {
        public static StoryManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        private int _storyIndex;
        private int _advancement;
        public bool IsDisplayingStory { private set; get; }

        public void Next()
        {
            var key = $"Story_{_storyIndex}-{_advancement}";
            if (Translate.Instance.Exists(key))
            {
                UIManager.Instance.DisplayText(key);
                _advancement++;
            }
            else
            {
                UIManager.Instance.HideStoryContainer();
            }
        }
    }
}
