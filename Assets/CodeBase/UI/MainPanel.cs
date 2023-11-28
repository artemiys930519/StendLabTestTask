using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.SceneRepository;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.UI
{
    public class MainPanel : ViewPanel, IMainViewPanel
    {
        #region Inspector

        [SerializeField] private List<PanelViewData> _panelViews;

        #endregion

        private ISceneRepository _sceneRepository;

        [Inject]
        private void Construct(ISceneRepository sceneRepository)
        {
            _sceneRepository = sceneRepository;
            _sceneRepository.RegisterMainUI(this);
        }

        public void SwitchPanel(Enumenators.PanelType panelType)
        {
            foreach (PanelViewData viewData in _panelViews)
            {
                if (viewData.PanelType == panelType)
                {
                    viewData.ViewPanel.ShowPanel();
                }
                else
                {
                    viewData.ViewPanel.HidePanel();
                }
            }
        }

        #region InnerClass

        [Serializable]
        public class PanelViewData
        {
            public ViewPanel ViewPanel;
            public Enumenators.PanelType PanelType;
        }

        #endregion
    }
}