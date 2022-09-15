using MoneyBuster.Gameplay;
using MoneyBuster.UI;
using UnityEngine;

namespace MoneyBuster.Manager
{
    public sealed class UIManager : Singleton<UIManager>
    {
        public UIObject levelCompletedPanel;
        public UIObject gamePanel;

        [Space] public UIText levelText;
        public UIScoreText scoreText;
        
        [Space] public UIObject shredText;
        public UIObject takeText;
    }
}