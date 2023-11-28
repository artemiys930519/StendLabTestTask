using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IFactory
    {
        public UniTask<GameObject> CreatePrefab(GameObject prefab, Vector3 position, Vector3 rotation);

    }
}