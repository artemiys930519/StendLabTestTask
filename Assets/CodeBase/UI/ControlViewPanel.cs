using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class ControlViewPanel : ViewPanel
    {
        #region Inspector

        [SerializeField] private ViewPanel _viewPanel;
        [SerializeField] private Button _applicationButton;

        #endregion

        private void OnEnable()
        {
            _applicationButton.onClick.AddListener(OnOpenApplication);
        }

        private void OnDisable()
        {
            _applicationButton.onClick.RemoveListener(OnOpenApplication);
        }

        private void OnOpenApplication()
        {
            _viewPanel.ShowPanel();
        }
    }
}