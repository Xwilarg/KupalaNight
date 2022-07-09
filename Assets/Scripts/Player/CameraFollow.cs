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

        private Vector3 _orPos;

        private void Awake()
        {
            _offset = _target.position - transform.position;
            _orPos = transform.position;
        }

        private void Update()
        {
            var p = _target.position - _offset;
            transform.position = new Vector3(Mathf.Clamp(p.x, _min, _max), _orPos.y, _orPos.z);
        }
    }
}
