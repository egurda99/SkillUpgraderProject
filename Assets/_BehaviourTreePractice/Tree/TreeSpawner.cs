using System.Collections.Generic;
using MyTimer;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BehaviourTreePractice
{
    public sealed class TreeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _treePrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private int _treesLimit = 5;


        [SerializeField] private float _spawnInterval = 2f;

        [SerializeField] private List<Transform> _zonePoints = new();

        [ShowInInspector] [ReadOnly] private Timer _timer;
        [ShowInInspector] [ReadOnly] private ActiveTreesProvider _activeTreesProvider;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(Timer timer, ActiveTreesProvider activeTreesProvider, DiContainer diContainer)
        {
            _activeTreesProvider = activeTreesProvider;
            _timer = timer;
            _timer.SetInterval(_spawnInterval);
            _timer.OnElapsed += SpawnTree;
            _timer.Start();
            _diContainer = diContainer;
        }

        private void Update()
        {
            _timer.Tick();
        }

        private void OnDestroy()
        {
            _timer.OnElapsed -= SpawnTree;
        }

        private void SpawnTree()
        {
            if (_treePrefab == null || _zonePoints.Count < 3 || _activeTreesProvider.Trees.Count >= _treesLimit)
                return;

            var point = GetRandomPointInPolygon();
            point.y = 0f;

            var tree = Instantiate(_treePrefab, point, Quaternion.identity, _container);

            var treeComponent = tree.GetComponent<Tree>();
            treeComponent.Init();
            _activeTreesProvider.OnTreeSpawned(treeComponent);
            _timer.Reset();
            _timer.Start();
        }

        private Vector3 GetRandomPointInPolygon()
        {
            var triangleCount = _zonePoints.Count - 2;
            var triangleIndex = Random.Range(0, triangleCount);

            var a = _zonePoints[0].position;
            var b = _zonePoints[triangleIndex + 1].position;
            var c = _zonePoints[triangleIndex + 2].position;

            return RandomPointInTriangle(a, b, c);
        }

        private Vector3 RandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
        {
            var r1 = Random.value;
            var r2 = Random.value;

            if (r1 + r2 > 1f)
            {
                r1 = 1f - r1;
                r2 = 1f - r2;
            }

            return a + r1 * (b - a) + r2 * (c - a);
        }

        private void OnDrawGizmos()
        {
            if (_zonePoints == null || _zonePoints.Count < 2)
                return;

            Gizmos.color = Color.green;
            for (var i = 0; i < _zonePoints.Count; i++)
            {
                var current = _zonePoints[i];
                var next = _zonePoints[(i + 1) % _zonePoints.Count];
                Gizmos.DrawLine(current.position, next.position);
            }
        }
    }
}
