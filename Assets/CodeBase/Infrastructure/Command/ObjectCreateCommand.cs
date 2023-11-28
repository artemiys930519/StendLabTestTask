using System.Threading;
using CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Command
{
    public class ObjectCreateCommand : IUndoCommand
    {
        private readonly IFactory _factory;
        private readonly MeshFilter _meshFilter;
        private readonly GameObject _objectPrefab;

        private readonly CancellationTokenSource _cancellationToken;

        public ObjectCreateCommand(GameObject objectPrefab, IFactory factory, MeshFilter meshFilter)
        {
            _cancellationToken = new();
            _objectPrefab = objectPrefab;
            _factory = factory;
            _meshFilter = meshFilter;
        }

        public async void Execute()
        {
            await InfinityObjectCreate();
        }

        public void Undo()
        {
            _cancellationToken.Cancel();
        }

        private async UniTask InfinityObjectCreate()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                GameObject prefabInstance =
                    await _factory.CreatePrefab(_objectPrefab, GetRandomMeshFilterPosition(), Vector3.zero);

                prefabInstance.transform.localScale =
                    new Vector3(Random.Range(0.1f, 2),
                        Random.Range(0.1f, 2),
                        Random.Range(0.1f, 2));
                
                await UniTask.Delay(2000);
            }
        }

        private Vector3 GetRandomMeshFilterPosition()
        {
            if (_meshFilter == null)
            {
                Debug.LogError("MehFilter is not assigned!");
                return Vector3.zero;
            }

            Vector3[] vertices = _meshFilter.mesh.vertices;
            int randomIndex = Random.Range(0, vertices.Length);
            Vector3 randomPosition = _meshFilter.transform.TransformPoint(vertices[randomIndex]);

            return randomPosition;
        }
    }
}