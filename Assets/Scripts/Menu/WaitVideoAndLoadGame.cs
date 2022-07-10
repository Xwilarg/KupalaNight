using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace UnityWithUkraine.Menu
{
    public class WaitVideoAndLoadGame : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<VideoPlayer>().loopPointReached += (e) =>
            {
                SceneManager.LoadScene("Main");
            };
        }
    }
}
