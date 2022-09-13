using System;
using MoneyBuster.Enums;
using MoneyBuster.Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace MoneyBuster.Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        public Camera MainCamera;
        
        private Holdable _currentHoldable;

        public GameState GameState { get; set; }

        protected override void Awake()
        {
            base.Awake();
            GameState = GameState.None;
        }

        private void Start()
        {
            GameState = GameState.Playing;
        }

        private void Update()
        {
            if (GameState != GameState.Playing)
                return;
            
            if (!_currentHoldable && Input.GetMouseButtonDown(0))
            {
                // We are checking touch count if more than 1, because player can use more than 1 finger.
                if (Input.touchCount > 1)
                    return;

                // Checking if we touch any holdable object
                if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo, 10f, LayerMask.GetMask("Holdable")))
                {
                    _currentHoldable = hitInfo.transform.GetComponent<Holdable>();
                    _currentHoldable.IsHolding = true;
                }
            }
            else if (_currentHoldable && Input.GetMouseButtonUp(0))
            {
                _currentHoldable.IsHolding = false;
                _currentHoldable = null;
            }
        }
    }
}