using System;
using UnityEngine;

namespace PoolSystem.Example
{
    public class Bullet : PoolObjectBase<Bullet>
    {
        [SerializeField] private float speed;

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
            if (!_isOpen) return;

            _rigidbody.velocity = transform.forward * speed;
        }

        protected override void OnSpawnObject()
        {
            _rigidbody.velocity = Vector3.zero;
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
