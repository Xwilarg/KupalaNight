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

        [SerializeField]
        private Transform[] _posInfo;

        [SerializeField]
        private Transform _reference;

        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _sr;
        private Camera _cam;

        /// <summary>
        /// Offset between the player pos and the tag
        /// </summary>
        private float _yOffset;

        private readonly Dictionary<ItemType, int> _inventory = new();

        /// <summary>
        /// Where the player need to go
        /// </summary>
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
            _posInfo = _posInfo.OrderBy(x => x.position.x).ToArray();
            _yOffset = _reference.position.y - transform.position.y;
        }

        private float GetYOffset(float x)
        {
            var indexBefore = _posInfo.Count(p => p.position.x < x) - 1;
            var indexAfter = indexBefore + 1;
            var relativePos = x - _posInfo[indexBefore].position.x;
            var distance = _posInfo[indexAfter].position.x - _posInfo[indexBefore].position.x;
            return Mathf.Lerp(_posInfo[indexBefore].position.y, _posInfo[indexAfter].position.y, relativePos / distance);
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
                transform.position = new Vector3(
                    x: transform.position.x,
                    y: GetYOffset(transform.position.x) - _yOffset,
                    z: transform.position.z
                );
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

        private void OnDrawGizmos()
        {
            if (_posInfo.Length < 2)
            {
                return;
            }
            Gizmos.color = Color.blue;
            for (int i = 0; i < _posInfo.Length - 1; i++)
            {
                Gizmos.DrawLine(_posInfo[i].position, _posInfo[i + 1].position);
            }
        }
    }
}
