using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]

    public class ViewPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            HidePanel();
        }

        public void ShowPanel()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void HidePanel()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}