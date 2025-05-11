using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float speed = 3;
        [SerializeField] private float jumpForce = 10;
        private Vector3 _direction = Vector3.zero;
        private Rigidbody _rigidbody;

        private void Awake()
            => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            var scaledDirection = _direction * acceleration;
            if (_rigidbody.linearVelocity.IgnoreY().magnitude < speed)
                _rigidbody.AddForce(scaledDirection, ForceMode.Force);
        }

        public void SetDirection(Vector3 direction)
            => _direction = direction;

        public bool GroundCheck()
            => _rigidbody.linearVelocity.y == 0f;

        public bool FallCheck()
             => _rigidbody.linearVelocity.y < 0f;

        public bool IdleCheck()
            => Mathf.Abs(_rigidbody.linearVelocity.x) <= .25f && Mathf.Abs(_rigidbody.linearVelocity.z) <= .25f;

        public IEnumerator Jump()
        {
            yield return new WaitForFixedUpdate();
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}