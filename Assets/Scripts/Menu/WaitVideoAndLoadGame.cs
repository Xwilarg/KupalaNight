using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace UnityWithUkraine.Menu
{
    public class WaitVideoAndLoadGame : MonoBehaviour
    {
        private void Awake()
        {
            var video = GetComponent<VideoPlayer>();
            video.url = Path.Combine(Application.streamingAssetsPath, "intro.mp4");
            video.loopPointReached += (e) =>
            {
                SceneManager.LoadScene("Main");
            };
            video.Play();
        }
    }
}
