using System;
using DG.Tweening;
using MoneyBuster.Manager;
using UnityEngine;

namespace MoneyBuster.Gameplay
{
    public abstract class Puttable : MonoBehaviour
    {
        [SerializeField] protected float duration;
        [SerializeField] protected Vector3 fromPosition;
        [SerializeField] protected Vector3 fromRotation;
        [SerializeField] protected Vector3 toPosition;

        public virtual Tweener Put(Money money)
        {
            money.transform.position = transform.TransformPoint(fromPosition);
            money.transform.rotation = Quaternion.Euler(fromRotation);
            return money.transform.DOMove(transform.TransformPoint(toPosition), duration);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.TransformPoint(fromPosition), .15f);
            Gizmos.DrawSphere(transform.TransformPoint(toPosition), .15f);
            Gizmos.DrawLine(transform.TransformPoint(fromPosition), transform.TransformPoint(toPosition));
        }
    }
}