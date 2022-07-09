using System.Collections.Generic;
using System.Linq;
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

        private readonly List<Tackable> _itemsInRange = new();

        private readonly Dictionary<ItemType, int> _inventory = new();

        public void AddItemInRange(Tackable item)
        {
            _itemsInRange.Add(item);
            UIManager.Instance.TogglePressToTake(true);
        }

        public void RemoveItemInRange(Tackable item)
        {
            _itemsInRange.Remove(item);
            if (!_itemsInRange.Any())
            {
                UIManager.Instance.TogglePressToTake(false);
            }
        }

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
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            if (StoryManager.Instance.IsDisplayingStory)
            {
                return;
            }
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
            if (value.performed)
            {
                if (StoryManager.Instance.IsDisplayingStory)
                {
                    StoryManager.Instance.Next();
                }
                else if (_itemsInRange.Any())
                {
                    AddToInventory(_itemsInRange[0].Item);
                    var go = _itemsInRange[0].gameObject;
                    RemoveItemInRange(_itemsInRange[0]);
                    Destroy(go);
                }
            }
        }
    }
}
