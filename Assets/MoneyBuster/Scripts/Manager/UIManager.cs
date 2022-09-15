using MoneyBuster.UI;
using UnityEngine;

namespace MoneyBuster.Manager
{
    public sealed class UIManager : Singleton<UIManager>
    {
        public UIObject levelCompletedPanel;
        public UIObject levelFailedPanel;
        public UIObject gamePanel;

        [Space] public UIObject shredText;
        public UIObject takeText;
    }
}