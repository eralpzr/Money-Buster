using System;
using DG.Tweening;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class MoneyStack : Puttable
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override Tweener Put(Money money)
        {
            var tweener = base.Put(money);
            _animator.SetTrigger("AddMoney");
            return tweener;
        }

        public void TriggerAnimation(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
    }
}