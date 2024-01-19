using PoolSystem.Extension;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoolSystem.Example
{
    public class PistolController : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzleTipReferenceTransform;
        [SerializeField] private float fireRate = 0.5f;
        
        private float _nextFireTime = 0f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q) && Time.time > _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + 1f / fireRate;
            }
            
            ResetRotation();
        }

        private void ResetRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 25f);
        }
        
        private void Fire()
        {
            float randomX = Random.Range(-1f, 1f) * 3f;
            float randomY = Random.Range(-1f, 1f) * 3f;
            Quaternion randomRotation = Quaternion.Euler(randomX, randomY, 0f);
            transform.rotation = randomRotation;
            
            bulletPrefab.GetPooledObject(muzzleTipReferenceTransform.position, muzzleTipReferenceTransform.rotation);
        }
    }
}
