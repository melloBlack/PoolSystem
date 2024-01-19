using UnityEngine;

namespace PoolSystem.Example
{
   public interface IConcreteObject
   {
      public void OnInteraction(Vector3 hitPoint);
   }
}
