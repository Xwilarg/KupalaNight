using UnityEngine;
using UnityEngine.InputSystem;
using UnityWithUkraine.SO;

namespace UnityWithUkraine.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            var x = value.ReadValue<Vector2>().x;
            _rb.velocity = new Vector2(x * _info.PlayerSpeed, _rb.velocity.y);
        }
    }
}
