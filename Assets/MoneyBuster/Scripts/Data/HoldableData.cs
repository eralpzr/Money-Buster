using MoneyBuster.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoneyBuster.Data
{
    [CreateAssetMenu(fileName = "Holdable Data", menuName = "Money Buster/Holdable Data")]
    public class HoldableData : SerializedScriptableObject
    {
        public float speed;
        public Vector3 offset;
        public Vector3 rotation;
    }
}