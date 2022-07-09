using UnityEngine;

namespace UnityWithUkraine.Player
{
    internal class CameraFollow : MonoBehaviour
    {
        private Vector3 _offset;

        [SerializeField]
        private Transform _target;

        private void Awake()
        {
            _offset = _target.position - transform.position;
        }

        private void Update()
        {
            transform.position = _target.position - _offset;
        }
    }
}
