using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class UVLightProp : Holdable
    {
        [SerializeField] private GameObject _purpleLight;
        
        protected override void OnHold()
        {
            _purpleLight.SetActive(true);
        }

        protected override void OnHoldUpdate()
        {
            
        }

        protected override void OnLeave()
        {
            _purpleLight.SetActive(false);
        }
    }
}