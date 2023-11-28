using CodeBase.Enums;

namespace CodeBase.Core.UI
{
    public interface IMainViewPanel
    {
        public void SwitchPanel(Enumenators.PanelType panelType);
        public void ShowPanel();
        public void HidePanel();
    }
}