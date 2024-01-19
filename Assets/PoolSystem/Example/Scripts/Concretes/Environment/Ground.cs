using PoolSystem.Extension;
using UnityEngine;
using UnityEngine.Serialization;

namespace PoolSystem.Example
{
    public class Ground : MonoBehaviour, IConcreteObject
    {
        [SerializeField] private SnowHole hole;

        public void OnInteraction(Vector3 hitPoint)
        {
            hole.GetPooledObject(hitPoint, Quaternion.identity);

        }
    }
}
