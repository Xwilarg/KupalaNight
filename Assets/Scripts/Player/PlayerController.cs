using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityWithUkraine.Item;
using UnityWithUkraine.SO;
using UnityWithUkraine.Story;

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
        private Camera _cam;

        private readonly Dictionary<ItemType, int> _inventory = new();

        private float _xObj;

        private void AddToInventory(ItemType item)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item]++;
            }
            else
            {
                _inventory.Add(item, 1);
            }
        }

        private void Awake()
        {
            Instance = this;
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
            _cam = Camera.main;
            _xObj = transform.position.x;
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(transform.position.x - _xObj) < _info.DistanceBeforeStop)
            {
                _rb.velocity = Vector3.zero;
                _anim.SetBool("IsWalking", false);
            }
            else
            {
                var goRight = transform.position.x < _xObj;
                _rb.velocity = (goRight ? 1f : -1f) * _info.PlayerSpeed * Vector2.right;
                _anim.SetBool("IsWalking", true);
                _sr.flipX = !goRight;
            }
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                if (StoryManager.Instance.IsDisplayingStory)
                {
                    StoryManager.Instance.Next();
                }
                else
                {
                    _xObj = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
                }
            }
        }
    }
}
