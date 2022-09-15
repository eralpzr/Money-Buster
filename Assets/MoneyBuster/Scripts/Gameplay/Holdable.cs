using System;
using MoneyBuster.Data;
using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public abstract class Holdable : MonoBehaviour
    {
        [SerializeField] protected HoldableData _data;
        
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
        protected abstract void OnHoldUpdate();
        protected abstract void OnLeave();
        
        protected virtual void Awake()
        {
            _camera = GameManager.Instance.mainCamera;

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
                    transform.position = Vector3.Lerp(transform.position, hitInfo.point + _data.offset, Time.deltaTime * _data.speed);
                    OnHoldUpdate();
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _initialPosition, Time.deltaTime * _data.speed);
            }
            
            transform.rotation = Quaternion.Lerp(transform.rotation, IsHolding ? Quaternion.Euler(_data.rotation) : _initialRotation, Time.deltaTime * _data.speed);
        }
    }
}