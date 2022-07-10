using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityWithUkraine.Player
{
    public class EndingTrigger : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (PlayerController.Instance.Count(Item.ItemType.Flower) >= 8)
                {
                    if (PlayerController.Instance.Contains(Item.ItemType.Scarf))
                    {
                        SceneManager.LoadScene("GoodEnding");
                    }
                    else
                    {
                        SceneManager.LoadScene("NeutralEnding");
                    }
                }
                else
                {
                    SceneManager.LoadScene("BadEnding");
                }
            }
        }
    }
}
