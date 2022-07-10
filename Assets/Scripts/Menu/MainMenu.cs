using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityWithUkraine.Translation;

namespace UnityWithUkraine.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _languageText;

        public void Play()
        {
            SceneManager.LoadScene("Intro");
        }

        private void Start()
        {
            UpdateLanguageDisplay();
        }

        public void ChangeLanguage()
        {
            if (Translate.Instance.CurrentLanguage == "english")
            {
                Translate.Instance.CurrentLanguage = "polish";
            }
            else if (Translate.Instance.CurrentLanguage == "polish")
            {
                Translate.Instance.CurrentLanguage = "french";
            }
            else
            {
                Translate.Instance.CurrentLanguage = "english";
            }
            UpdateLanguageDisplay();
        }

        private void UpdateLanguageDisplay()
        {
            _languageText.text = $"{Translate.Instance.Tr("language")} ({Translate.Instance.Tr("currentLanguage")})";
        }
    }
}
