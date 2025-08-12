using UnityEngine;

namespace Game.Tutorial.Gameplay
{
    public sealed class NavigationArrow : MonoBehaviour
    {
        [SerializeField] private GameObject _rootGameObject;

        [SerializeField] private Transform _rootTransform;


        public GameObject RootGameObject => _rootGameObject;

        public Transform RootTransform => _rootTransform;


        public void Show()
        {
            _rootGameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            _rootTransform.position = position;
        }

        public void LookAt(Vector3 targetPosition)
        {
            var distanceVector = targetPosition - _rootTransform.position;
            distanceVector.y = 0;
            _rootTransform.rotation = Quaternion.LookRotation(distanceVector.normalized, Vector3.up);
        }
    }
}
