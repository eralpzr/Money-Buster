using DG.Tweening;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class PaperShredder : Puttable
    {        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public override Tweener Put(Money money)
        {
            var tweener = base.Put(money);
            _animator.SetTrigger("MoneyShredStart");
            return tweener;
        }
    }
}