using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityWithUkraine.Item;
using UnityWithUkraine.SO;

namespace UnityWithUkraine.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField]
        private PlayerInfo _info;

        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _sr;

        private readonly List<Tackable> _itemsInRange = new();

        public void AddItem(Tackable item)
        {
            _itemsInRange.Add(item);
            UIManager.Instance.TogglePressToTake(true);
        }

        public void RemoveItem(Tackable item)
        {
            _itemsInRange.Remove(item);
            if (!_itemsInRange.Any())
            {
                UIManager.Instance.TogglePressToTake(false);
            }
        }

        private void Awake()
        {
            Instance = this;
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            var x = value.ReadValue<Vector2>().x;
            _anim.SetBool("IsWalking", x != 0f);
            if (x > 0f)
            {
                _sr.flipX = false;
            }
            else if (x < 0f)
            {
                _sr.flipX = true;
            }
            _rb.velocity = new Vector2(x * _info.PlayerSpeed, _rb.velocity.y);
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && _itemsInRange.Any())
            {
                // TODO: Add in inventory
                var go = _itemsInRange[0].gameObject;
                RemoveItem(_itemsInRange[0]);
                Destroy(go);
            }
        }
    }
}
