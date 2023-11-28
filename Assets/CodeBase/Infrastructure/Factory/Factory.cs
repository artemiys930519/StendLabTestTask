using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class Factory : IFactory
    {
        public async UniTask<GameObject> CreatePrefab(GameObject prefab, Vector3 position, Vector3 rotation)
        {
            var untc = new UniTaskCompletionSource<GameObject>();
            GameObject prefabInstance = Object.Instantiate(prefab);
            prefabInstance.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
            untc.TrySetResult(prefabInstance);

            return await untc.Task;
        }
    }
}