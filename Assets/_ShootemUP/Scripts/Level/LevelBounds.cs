using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform leftBorder;

        [SerializeField] private Transform rightBorder;

        [SerializeField] private Transform downBorder;

        [SerializeField] private Transform topBorder;

        public bool InBounds(Vector2 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > leftBorder.position.x
                   && positionX < rightBorder.position.x
                   && positionY > downBorder.position.y
                   && positionY < topBorder.position.y;
        }
    }
}
