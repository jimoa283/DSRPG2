using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="AttackActionData",menuName ="Attack/AttackAction")]
    public class AttackActionData : ScriptableObject
    {
        public string attackActionName;
        public bool canFirstAttack;
        public int priorityLevel;
        public int hitLevel;
        public AttackType attackType;
        public int hitCount;
        public InputBufferKey[] inputBufferKeys;
        public float openAttackColliderTime;
        public float closeAttackColliderTime;
        public float openRotateTime;
        public float closeRotateTime;
        public float startAttackBackTime;
        public float maxRotateAngle;
        public AnimTrigger animTrigger;
        public string[] nextAttackActionNames;

        [System.Serializable]
        public struct AnimTrigger
        {
            public string animTypeName;
            public AnimationClip attackAnimationClip;
            public float speedValue;
            public int transValue;
            public bool defenseValue;
        }
    }
}

