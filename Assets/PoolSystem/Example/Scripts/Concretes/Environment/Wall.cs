using PoolSystem.Extension;
using UnityEngine;
using UnityEngine.Serialization;

namespace PoolSystem.Example
{
    public class Wall : MonoBehaviour, IConcreteObject
    {
        [SerializeField] private WallHole hole;
        
        public void OnInteraction(Vector3 hitPoint)
        {
            hole.GetPooledObject(hitPoint, Quaternion.identity);
        }
    }
}
