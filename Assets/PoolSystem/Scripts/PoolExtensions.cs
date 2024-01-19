//  * Information
//  * The Generic Pool presented here is created to allow the creation of queues for different types.
//  * It also places any desired object type into these queues and pulls it from the queue when needed.
//  * Usage:
//  * - Create a class of any type. Then inherit PoolObject class in your class. (Implements => public class SamplePoolClass : PoolObjectBase<PoolObjectBase>
//  * 
//  * Created by: İsmail Hakkı Kara (Mello Black)
//  * Date: [January, 2024]

using UnityEngine;

namespace PoolSystem.Extension
{
    /// <summary>
    /// Contains extension methods for the Pool class to simplify the process of retrieving pooled objects.
    /// </summary>
    public static class PoolExtensions
    {
        /// <summary>
        /// A private instance of the Pool class is used for object pooling operations.
        /// </summary>
        private static readonly Pool Pool = new Pool();

        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        private static PoolObjectBase<T> GetObject<T>(this T prefab) where T : PoolObjectBase<T>, new()
        {
            return Pool.GetObject(prefab);
        }
        
        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        public static PoolObjectBase<T> GetPooledObject<T>(this T prefab) where T : PoolObjectBase<T>, new()
        {
            PoolObjectBase<T> poolObject = GetObject(prefab);
            poolObject.Spawn(null, Vector3.zero, Quaternion.identity);
            poolObject.transform.parent = null;
            poolObject.ReturnToQueue += Pool.ReturnToQueue;
            return poolObject;
        }
        
        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <param name="parent"> Object parent. </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        public static PoolObjectBase<T> GetPooledObject<T>(this T prefab, Transform parent) where T : PoolObjectBase<T>, new()
        {
            PoolObjectBase<T> poolObject = GetObject(prefab);
            poolObject.Spawn(parent, Vector3.zero, Quaternion.identity);
            poolObject.ReturnToQueue += Pool.ReturnToQueue;
            return poolObject;
        }
        
        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <param name="position"> Set object spawn position </param>
        /// <param name="rotation"> Set object spawn rotation </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        public static PoolObjectBase<T> GetPooledObject<T>(this T prefab, Vector3 position, Quaternion rotation) where T : PoolObjectBase<T>, new()
        {
            PoolObjectBase<T> poolObject = GetObject(prefab);
            poolObject.Spawn(null, position, rotation);
            poolObject.transform.parent = null;
            poolObject.ReturnToQueue += Pool.ReturnToQueue;
            return poolObject;
        }

        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <param name="parent"> Object parent. </param>
        /// <param name="position"> Set object spawn position </param>
        /// <param name="rotation"> Set object spawn rotation </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        public static PoolObjectBase<T> GetPooledObject<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : PoolObjectBase<T>, new()
        {
            PoolObjectBase<T> poolObject = GetObject(prefab);
            poolObject.Spawn(parent, position, rotation);
            poolObject.ReturnToQueue += Pool.ReturnToQueue;
            return poolObject;
        }
    }
}
