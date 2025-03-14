using UnityEngine;

namespace ShootEmUp
{
    public sealed class TransformRandomizer
    {
        public Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}