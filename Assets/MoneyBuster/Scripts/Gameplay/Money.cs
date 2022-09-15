using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public sealed class Money : Holdable
    {
        private const float ClosestDistance = 2f;
        
        public bool isFake;
        
        protected override void OnHold()
        {
            
        }

        protected override void OnHoldUpdate()
        {
            var closestPuttable = GetClosestPuttable(ClosestDistance);
            if (closestPuttable == null)
            {
                UIManager.Instance.takeText.gameObject.SetActive(false);
                UIManager.Instance.shredText.gameObject.SetActive(false);
                return;
            }
            
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
            
            var closestPuttable = GetClosestPuttable(ClosestDistance);
            if (closestPuttable == null)
                return;

            var success = (isFake && closestPuttable is PaperShredder) || (!isFake && closestPuttable is MoneyStack);
            var tweener = closestPuttable.Put(this);
            tweener.onComplete += () =>
                                  {
                                      GameManager.Instance.StartCoroutine(GameManager.Instance.CompleteLevelCoroutine(!success));
                                      Destroy(gameObject);
                                  };

            GameManager.Instance.GiveScore(success ? 10 : -10);
            
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