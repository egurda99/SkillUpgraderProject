using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    public sealed class PointerManager
    {
        private readonly GameObject _pointer;


        public PointerManager(GameObject pointer)
        {
            _pointer = pointer;
            _pointer.SetActive(false);
        }

        [Button]
        public void ShowPointer(Transform targetPoint)
        {
            ShowPointer(targetPoint.position, targetPoint.rotation);
        }

        public void ShowPointer(Vector3 position, Quaternion rotation)
        {
            var pointerTransform = _pointer.transform;
            pointerTransform.position = position;
            pointerTransform.rotation = rotation;

            _pointer.SetActive(true);
        }

        public void HidePointer()
        {
            _pointer.SetActive(false);
        }
    }
}
