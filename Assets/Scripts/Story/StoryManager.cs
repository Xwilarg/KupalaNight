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
        private string _currentKey;

        public void ReadText(string key)
        {
            _currentKey = key;
            _advancement = 0;
            Next();
        }

        public void Next()
        {
            var key = $"{_currentKey}{_advancement}";
            if (Translate.Instance.Exists(key))
            {
                IsDisplayingStory = true;
                UIManager.Instance.DisplayText(Translate.Instance.Tr(key));
                _advancement++;
            }
            else
            {
                IsDisplayingStory = false;
                UIManager.Instance.HideStoryContainer();
            }
        }
    }
}
