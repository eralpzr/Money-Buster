using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class Money : Holdable
    {
        public bool isFake;
        
        protected override void OnHold()
        {
            
        }

        protected override void OnHoldUpdate()
        {
            var closestPuttable = GetClosestPuttable(2.5f);
            if (closestPuttable == null)
                return;
            
            if (closestPuttable is PaperShredder)
            {
                UIManager.Instance.takeText.gameObject.SetActive(false);
                UIManager.Instance.shredText.gameObject.SetActive(true);
            }
            else if (closestPuttable is MoneyStack)
            {
                UIManager.Instance.shredText.gameObject.SetActive(false);
                UIManager.Instance.takeText.gameObject.SetActive(true);
            }
        }

        protected override void OnLeave()
        {
            UIManager.Instance.shredText.gameObject.SetActive(false);
            UIManager.Instance.takeText.gameObject.SetActive(false);
            
            var closestPuttable = GetClosestPuttable(2.5f);
            if (closestPuttable == null)
                return;

            var failed = isFake && closestPuttable is PaperShredder;
            var tweener = closestPuttable.Put(this);
            tweener.onComplete += () =>
                                  {
                                      GameManager.Instance.StartCoroutine(GameManager.Instance.CompleteLevelCoroutine(failed));
                                      Destroy(gameObject);
                                  };

            enabled = false;
        }

        private Puttable GetClosestPuttable(float distance)
        {
            var shredderDistance = Vector3.Distance(transform.position, GameManager.Instance.paperShredder.transform.position);
            var stackDistance = Vector3.Distance(transform.position, GameManager.Instance.moneyStack.transform.position);

            // If we close to both of them, select the closest one.
            if (shredderDistance < distance && stackDistance < distance)
                return shredderDistance < stackDistance ? GameManager.Instance.paperShredder : GameManager.Instance.moneyStack;
            
            if (shredderDistance < distance)
                return GameManager.Instance.paperShredder;

            if (shredderDistance < distance)
                return GameManager.Instance.moneyStack;

            return null;
        }
    }
}