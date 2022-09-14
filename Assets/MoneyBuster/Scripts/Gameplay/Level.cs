using System;
using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class Level : MonoBehaviour
    {
        public Money money;

        private void Awake()
        {
            GameManager.Instance.CurrentLevel = this;
        }
    }
}