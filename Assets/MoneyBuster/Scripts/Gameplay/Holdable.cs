using System;
using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public abstract class Holdable : MonoBehaviour
    {
        private const float Speed = 10f;
        
        [SerializeField] protected Vector3 _holdOffset;
        [SerializeField] protected Vector3 _holdRotation;
        
        protected Vector3 _initialPosition;
        protected Quaternion _initialRotation;

        private bool _isHolding;
        private Camera _camera;

        public bool IsHolding
        {
            get => _isHolding;
            set
            {
                _isHolding = value;
                
                if (_isHolding)
                    OnHold();
                else
                    OnLeave();
            }
        }

        protected abstract void OnHold();
        protected abstract void OnLeave();
        
        protected virtual void Awake()
        {
            _camera = GameManager.Instance.MainCamera;

            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        protected virtual void Update()
        {
            if (IsHolding)
            {
                // Checking if we touching ground
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo, 10f, LayerMask.GetMask("Ground")))
                {
                    transform.position = Vector3.Lerp(transform.position, hitInfo.point + _holdOffset, Time.deltaTime * Speed);
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _initialPosition, Time.deltaTime * Speed);
            }
            
            transform.rotation = Quaternion.Lerp(transform.rotation, IsHolding ? Quaternion.Euler(_holdRotation) : _initialRotation, Time.deltaTime * Speed);
        }
    }
}