using UnityEngine;

namespace PoolSystem.Example
{
    public class WallHole : PoolObjectBase<WallHole>
    {
        protected override void OnSpawnObject()
        {
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 2f);

            if (transform.localScale.x <= 0.05f)
            {
                ReturnToQueue?.Invoke(this);
            }
        }
    }
}
