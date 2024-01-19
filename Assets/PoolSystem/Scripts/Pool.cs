//  * Information
//  * The Generic Pool presented here is created to allow the creation of queues for different types.
//  * It also places any desired object type into these queues and pulls it from the queue when needed.
//  * Usage:
//  * - Create a class of any type. Then inherit PoolObject class in your class. (Implements => public class SamplePoolClass : PoolObjectBase<PoolObjectBase>
//  * 
//  * Created by: İsmail Hakkı Kara (Mello Black)
//  * Date: [January, 2024]

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolSystem
{
    /// <summary>
    /// Represents a global object repository for object reuse in Unity.
    /// Objects of a specified type can be retrieved from the pool and returned when no longer in use, reducing the overhead of object instantiation and destruction.
    /// </summary>
    public sealed class Pool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pool"/> class.
        /// Creates a parent transform for the waiting pool objects and initializes the dictionary for type queues.
        /// </summary>
        public Pool()
        {
            _poolParent = new GameObject { name = "PoolParent" }.transform;
            _queues = new Dictionary<Type, object>();
        }
        
        /// <summary>
        /// Parent transform for the waiting pool objects
        /// </summary>
        private readonly Transform _poolParent;

        /// <summary>
        /// Queue collection for all type queues
        /// </summary>
        private readonly Dictionary<Type, object> _queues;

        /// <summary>
        /// Adding a new queue type of T in queue list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void AddNewQueue<T>()
        {
            Queue<T> queue = new Queue<T>();
            _queues[typeof(T)] = queue;
        }

        /// <summary>
        /// Returns the queue of type T from the queue list. If the queue list not contains the type T, then creates a new one and returned it.
        /// </summary>
        /// <returns> queue of T type </returns>
        private Queue<T> GetQueue<T>() where T : class
        {
            if (!_queues.ContainsKey(typeof(T)))
            {
                AddNewQueue<T>();
            }

            return (Queue<T>)_queues[typeof(T)];
        }

        /// <summary>
        /// Returns the pooled object from the queue of type T
        /// </summary>
        /// <param name="prefab"> Object prefab. Necessary for instantiate of object. </param>
        /// <typeparam name="T"> Any class that is inherited from the pool object. </typeparam>
        /// <returns> pool object of T type </returns>
        public PoolObjectBase<T> GetObject<T>(T prefab) where T : PoolObjectBase<T>, new()
        {
            Queue<T> currentTypeOfQueue = GetQueue<T>();

            if (currentTypeOfQueue.Count == 0)
            {
                T newPoolObject = Object.Instantiate(prefab);

                currentTypeOfQueue.Enqueue(newPoolObject);
            }

            T poolObject = currentTypeOfQueue.Dequeue();
            poolObject.gameObject.SetActive(true);

            return poolObject;
        }

        /// <summary>
        /// Objects whose task is completed are added back to the queue.
        /// </summary>
        /// <param name="poolObject"> The object to add back to the queue </param>
        /// <typeparam name="T"></typeparam>
        public void ReturnToQueue<T>(PoolObjectBase<T> poolObject) where T : PoolObjectBase<T>, new()
        {
            // Get queue type of T
            Queue<T> currentTypeOfQueue = GetQueue<T>();

            // Gets back to the queue
            currentTypeOfQueue.Enqueue(poolObject as T);

            // The object unsubscribes from the action because its task is completed.
            poolObject.ReturnToQueue -= ReturnToQueue;
            poolObject.gameObject.SetActive(false);

            if (poolObject.transform.parent == null)
            {
                poolObject.transform.parent = _poolParent;
            }
        }
    }
}