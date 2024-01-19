using PoolSystem.Extension;
using UnityEngine;

namespace PoolSystem.Example
{
    public class RocketLauncherController : MonoBehaviour
    {
        [SerializeField] private Rocket rocketPrefab;
        [SerializeField] private Transform muzzleTipReferenceTransform;
        [SerializeField] private float fireRate = 0.5f;
        
        private float _nextFireTime = 0f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.W) && Time.time > _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + 1f / fireRate;
            }
            
            ResetRotation();
        }

        private void ResetRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 15f);
        }

        private void Fire()
        {
            float randomX = Random.Range(0.5f, 1f) * -25f;
            Quaternion randomRotation = Quaternion.Euler(randomX, 0f, 0f);
            transform.rotation = randomRotation;
            
            rocketPrefab.GetPooledObject(muzzleTipReferenceTransform.position, muzzleTipReferenceTransform.rotation);
        }
    }
}
