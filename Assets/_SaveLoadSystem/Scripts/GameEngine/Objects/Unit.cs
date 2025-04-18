using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour
    {
        [SerializeField] private string _type;
        [SerializeField] [ReadOnly] private string _id;
        [SerializeField] private int _hitPoints;

        public string ID => _id;

        public string Type
        {
            get => _type;
        }

        public int HitPoints
        {
            get => _hitPoints;
            set => _hitPoints = value;
        }

        public Vector3 Position
        {
            get => transform.position;
        }

        public Vector3 Rotation
        {
            get => transform.eulerAngles;
        }

        public void Setup(string type, string id, int hitPoints)
        {
            _type = type;
            _id = id;
            _hitPoints = hitPoints;
        }

        public void Setup(string type, int hitPoints)
        {
            _type = type;
            _hitPoints = hitPoints;
        }

        [Button]
        public void GenerateId()
        {
            var instanceId = GetInstanceID().ToString();
            _id = IdGenerator.Generate<Unit>("Unit_") + "_" + instanceId;
        }

        private void Reset()
        {
            _type = name;
            _hitPoints = 10;
        }
    }
}
