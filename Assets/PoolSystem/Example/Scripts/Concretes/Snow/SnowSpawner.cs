using PoolSystem.Extension;
using UnityEngine;
using UnityEngine.Serialization;

namespace PoolSystem.Example
{
    public class SnowSpawner : MonoBehaviour
    {
        [SerializeField] private Snow snowPrefab;
        [SerializeField] private float startPosY;
        [SerializeField] private Vector2 xBounds;
        [SerializeField] private Vector2 zBounds;
        [SerializeField] private float fallRate = 0.5f;
        
        private float _nextFireTime = 0f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.E) && Time.time > _nextFireTime)
            {
                CreateSnowFlake();
                _nextFireTime = Time.time + 1f / fallRate;
            }
        }

        private void CreateSnowFlake()
        {
            snowPrefab.GetPooledObject(GetRandomPosition(), GetRandomRotation());
        }

        private Vector3 GetRandomPosition()
        {
            float x = Random.Range(xBounds.x, xBounds.y);
            float z = Random.Range(zBounds.x, zBounds.y);

            return new Vector3(x, startPosY, z);
        }

        private Quaternion GetRandomRotation()
        {
            float randomX = Random.Range(0f, 360f);
            float randomY = Random.Range(0f, 360f);
            float randomZ = Random.Range(0f, 360f);

            return Quaternion.Euler(randomX, randomY, randomZ);
        }
    }
}
