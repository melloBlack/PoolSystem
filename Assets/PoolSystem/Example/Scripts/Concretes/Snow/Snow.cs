using UnityEngine;

namespace PoolSystem.Example
{
    public class Snow : PoolObjectBase<Snow>
    {
        [SerializeField] private Vector2 landingPowerRange;

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

        protected override void OnSpawnObject()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(GetLandingPower() * Vector3.down);
            _isOpen = true;
        }
        
        private void DeadObject()
        {
            ReturnToQueue?.Invoke(this);
            
            _isOpen = false;
            _rigidbody.velocity = Vector3.zero;
        }

        private float GetLandingPower()
        {
            return Random.Range(landingPowerRange.x, landingPowerRange.y);
        }
    }
}
