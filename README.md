# PoolSystem

**It is offered as open source. You can use it as you wish. Contact me when you need help.**

You can also download unitypackage.
[Unity Package İndir](https://drive.google.com/file/d/1YPd4AH_qKwc5QdsgDXFjYs8i7FGrXSGQ/view?usp=sharing)

## Usage
- Create a class that you want to pool.

 ```csharp
  public class SamplePoolObject
  {

  }
```

- Derive this class from the PoolObjectBase class.
- The OnSpawnObject method is called once every time an object is retrieved from the pool.

 ```csharp
    public class SamplePoolObject : PoolObjectBase<SamplePoolObject>
    {
        /// <summary>
        /// Abstract method called when the object is pulled.
        /// </summary>
        protected override void OnSpawnObject()
        {
            // You can write the operations you want the object to perform after being called within this method.
        }
    }
```

- When you are done with the object, trigger the ReturnToQueue action found in the base class.

```csharp
    public class SamplePoolObject : PoolObjectBase<SamplePoolObject>
    {
        /// <summary>
        /// Abstract method called when the object is pulled.
        /// </summary>
        protected override void OnSpawnObject()
        {
            // You can write the operations you want the object to perform after being called within this method.
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("SampleTag")
            {
                ReturnToQueue?.Invoke(this);
            }
        }
    }
```

- Open a controller class where you will create objects.
- In this class, create a variable for the class you want to derive, which you can pull from the Inspector. Assign the object's prefab to this variable through the Inspector.
- Use the following code to instantiate the object: YourClass.GetPooledObject(parameters);

```csharp
   public class PistolController : MonoBehaviour
    {
        [SerializeField] private SamplePoolObject samplePoolObject; // Your prefab

        private void PullObjectInQueue()
        {
            // Option 1
            samplePoolObject.GetPooledObject();

            Transform parent = transform;

            // Option 2
            samplePoolObject.GetPooledObject(parent);

            Vector3 position = new Vector3(0, 5, 0);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 90, 45));

            // Option 3
            samplePoolObject.GetPooledObject(position, rotation);

            // Option 4
            samplePoolObject.GetPooledObject(parent, position, rotation);
        }
    }
```

- You're ready to bend!

**Happy Code Bending!**
