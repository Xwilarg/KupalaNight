using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityWithUkraine.Item;
using UnityWithUkraine.SO;
using UnityWithUkraine.Story;
using UnityWithUkraine.Translation;

namespace UnityWithUkraine.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField]
        private PlayerInfo _info;

        [SerializeField]
        private LevelData[] _levelData;

        private Rigidbody2D _rb;
        private Animator _anim;
        private SpriteRenderer _sr;
        private Camera _cam;

        private List<Takable> _interactibles;

        private int _currentLevel;

        public (int Min, int Max) CameraMinMax => (CurrentData.Min, CurrentData.Max);

        private LevelData CurrentData => _levelData[_currentLevel];

        /// <summary>
        /// Offset between the player pos and the tag
        /// </summary>
        private float _yOffset;

        /// <summary>
        /// Object we clicked to interact with
        /// </summary>
        private Takable _target;

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
            _yOffset = CurrentData.Reference.position.y - transform.position.y;
            _interactibles = FindObjectsOfType<Takable>().ToList();
            Translate.Instance.Init();
        }

        public void MoveToZone(int index)
        {
            _currentLevel = index;
            _xObj = CurrentData.Reference.position.x;
            transform.position = new Vector3(
                x: CurrentData.Reference.position.x,
                y: GetYOffset(CurrentData.Reference.position.x) - _yOffset,
                z: transform.position.z
            );
        }

        private float GetYOffset(float x)
        {
            var indexBefore = CurrentData.PosInfo.Count(p => p.position.x < x) - 1;
            var indexAfter = indexBefore + 1;
            var relativePos = x - CurrentData.PosInfo[indexBefore].position.x;
            var distance = CurrentData.PosInfo[indexAfter].position.x - CurrentData.PosInfo[indexBefore].position.x;
            return Mathf.Lerp(CurrentData.PosInfo[indexBefore].position.y, CurrentData.PosInfo[indexAfter].position.y, relativePos / distance);
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(transform.position.x - _xObj) < _info.DistanceBeforeStop)
            {
                if (_target != null)
                {
                    if (_target.Item != ItemType.None)
                    {
                        AddToInventory(_target.Item);
                    }
                    if (!string.IsNullOrWhiteSpace(_target.VNToken))
                    {
                        StoryManager.Instance.ReadText(_target.VNToken);
                    }
                    if (_target.DeleteOnInteraction)
                    {
                        _interactibles.Remove(_target);
                        Destroy(_target.gameObject);
                    }
                    _target = null;
                }
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
                    _target = _interactibles.FirstOrDefault(x => Mathf.Abs(x.transform.position.x - _xObj) < _info.DistanceClickForInterract);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            foreach (var level in _levelData)
            {
                for (int i = 0; i < level.PosInfo.Length - 1; i++)
                {
                    Gizmos.DrawLine(level.PosInfo[i].position, level.PosInfo[i + 1].position);
                }
            }
        }
    }
}
