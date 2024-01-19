using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PoolSystem.Example
{
    public class Rocket : PoolObjectBase<Rocket>
    {
        [SerializeField] private float throwPower;

        private Rigidbody _rigidbody;
        private bool _isOpen;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isOpen) return;

            if (other.TryGetComponent(out IConcreteObject iConcreteObject))
            {
                iConcreteObject.OnInteraction(other.ClosestPoint(transform.position));
                DeadObject();
            }
        }
        
        private void FixedUpdate()
        {
            _rigidbody.angularVelocity = Vector3.right * Mathf.Abs((_rigidbody.velocity.y * 0.07f));
        }

        protected override void OnSpawnObject()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(throwPower * transform.up);
            _isOpen = true;
        }
        
        private void DeadObject()
        {
            ReturnToQueue?.Invoke(this);
            
            _isOpen = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
