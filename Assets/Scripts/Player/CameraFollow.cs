using UnityEngine;

namespace UnityWithUkraine.Player
{
    internal class CameraFollow : MonoBehaviour
    {
        private Vector3 _offset;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _min, _max;

        private float _orrY;

        private void Awake()
        {
            _offset = _target.position - transform.position;
            _orrY = transform.position.y;
        }

        private void Update()
        {
            var p = _target.position - _offset;
            transform.position = new Vector3(Mathf.Clamp(p.x, _min, _max), _orrY, 0f);
        }
    }
}
