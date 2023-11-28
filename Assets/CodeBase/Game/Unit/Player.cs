using CodeBase.Services.SceneRepository;
using UnityEngine;
using Zenject;

namespace CodeBase.Game.Unit
{
    public class Player : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private FirstPersonController _firstPersonController;

        #endregion

        [Inject]
        private void Construct(ISceneRepository sceneRepository)
        {
            sceneRepository.RegisterPlayer(_firstPersonController);
        }
    }
}