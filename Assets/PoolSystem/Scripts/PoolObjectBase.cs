//  * Information
//  * The Generic Pool presented here is created to allow the creation of queues for different types.
//  * It also places any desired object type into these queues and pulls it from the queue when needed.
//  * Usage:
//  * - Create a class of any type. Then inherit PoolObject class in your class. (Implements => public class SamplePoolClass : PoolObjectBase<PoolObjectBase>
//  * 
//  * Created by: İsmail Hakkı Kara (Mello Black)
//  * Date: [January, 2024]

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoolSystem
{
    /// <summary>
    /// Abstract base class for objects managed by an object pool.
    /// </summary>
    /// <typeparam name="T">Type of the derived class.</typeparam>
    public abstract class PoolObjectBase<T> : MonoBehaviour where T : new()
    {
        /// <summary>
        /// The delegate (Action) property, that returns the object to the pool.
        /// </summary>
        public Action<PoolObjectBase<T>> ReturnToQueue { get; set; }

        /// <summary>
        /// This method triggers when the pooled object is pulled. The specified parent, position, and return information are processed into the object and OnSpawnObject is triggered.
        /// </summary>
        /// <param name="parent"> Parent transform of the pulled object. </param>
        /// <param name="position"> Position of the pulled object. </param>
        /// <param name="rotation"> Rotation of the pulled object. </param>
        public void Spawn(Transform parent, Vector3 position, Quaternion rotation)
        {
            transform.parent = parent;
            transform.localPosition = position;
            transform.rotation = rotation;
            OnSpawnObject();
        }
        
        /// <summary>
        /// Abstract method called when the object is pulled.
        /// </summary>
        protected abstract void OnSpawnObject();
    }
}