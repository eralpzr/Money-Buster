using System;
using MoneyBuster.Enums;
using MoneyBuster.Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MoneyBuster.Manager
{
    public sealed class GameManager : Singleton<GameManager>
    {
        public bool isDebug;
        public int debugLevel;
        
        [Space] public Camera MainCamera;
        [SerializeField] private string[] _levels;

        private Holdable _currentHoldable;
        private string _currentLevelScene;

        public GameState GameState { get; set; }
        public Level CurrentLevel { get; set; }

        protected override void Awake()
        {
            base.Awake();
            GameState = GameState.None;
        }
        
        private void Start()
        {
            if (isDebug)
            {
                StartLevel(_levels[(debugLevel - 1) % _levels.Length]);
                return;
            }
            
            // We are looping levels when player reached last level
            StartLevel(_levels[(Progression.Level - 1) % _levels.Length]);
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
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            // Checking if only level scene loaded
            if (scene.name.Equals(_currentLevelScene))
            {
                UIManager.Instance.gamePanel.gameObject.SetActive(true);
                GameState = GameState.Playing;
            }
        }

        public void StartLevel(string levelScene)
        {
            // Unload if any active level scene and start new level
            if (!string.IsNullOrWhiteSpace(_currentLevelScene))
            {
                SceneManager.UnloadSceneAsync(_currentLevelScene);

                if (_currentHoldable)
                    _currentHoldable.IsHolding = false;
                
                _currentHoldable = null;
                _currentLevelScene = null;
            }
            
            SceneManager.LoadScene(_currentLevelScene = levelScene, LoadSceneMode.Additive);
        }
    }
}