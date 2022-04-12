using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class WeaponData : MonoBehaviour
    {
        public List<AttackActionData> singleAttackActionDatas;
        public List<AttackActionData> bothAttackActionDatas;
        public WeaponType weaponType;
        public LocomotionType locomotionType;
        public Collider weaponCollider;
        public bool canBothHand;
        public bool canShieldBack;
        public WeaponManager weaponManager;
        public string targetTag;

        public void Init(WeaponManager weaponManager)
        {
            this.weaponManager = weaponManager;
            weaponCollider = GetComponent<Collider>();
            targetTag = weaponManager.targetTag;
            weaponCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider col)
        {          

            if (!col.CompareTag(targetTag))
                return;

            CharacterController target = col.GetComponentInParent<CharacterController>();

            AttackManager attackManager = weaponManager.character.attackManager;

            if (!attackManager.hitTargetCountDic.ContainsKey(target))
                attackManager.hitTargetCountDic.Add(target, 0);

            if (attackManager.hitTargetCountDic[target] >= attackManager.currentAttackAction.hitCount)
                 return;

            attackManager.hitTargetCountDic[target]++;

            HitManager hitManager = target.hitManager;

            if (!hitManager.canCounterBack)
                hitManager.Hit(weaponManager.character, transform);
            else
                weaponManager.character.stateManager.isBeCounterBack = true;
        }

        public void CloseCollider()
        {
            //Debug.Log("OK");
            weaponCollider.enabled = false;
        }

        public void OpenCollider()
        {
            //Debug.Log("YU");
            weaponCollider.enabled = true;
        }
    }
}

