using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class AttackManager : MonoBehaviour
    {
        public CharacterController character;
        private WeaponManager weaponManager;
        public AttackActionData currentAttackAction;
        public AttackActionData nextAttackAction;
        public int currentHitLevel;
        public int currentAttackMutipler;
        public Vector3 startAttackDir;
        public Dictionary<CharacterController, int> hitTargetCountDic=new Dictionary<CharacterController, int>();

        public Dictionary<string, AttackActionData> currentDic=null;
        public Dictionary<string, AttackActionData> singleAttackActionDataDic = new Dictionary<string, AttackActionData>();
        public Dictionary<string, AttackActionData> bothAttackActionDataDic = new Dictionary<string, AttackActionData>();

        public void Init(CharacterController character)
        {
            this.character = character;
            weaponManager = character.weaponManager;
            SetAttackActionData(character.weaponManager.currentAttackWeapon.singleAttackActionDatas, character.weaponManager.currentAttackWeapon.bothAttackActionDatas);
        }

        public void SetAttackActionData(List<AttackActionData> singleList,List<AttackActionData> bothList)
        {
            singleAttackActionDataDic.Clear();

            foreach(var data in singleList)
            {
                singleAttackActionDataDic.Add(data.attackActionName, data);
            }

            bothAttackActionDataDic.Clear();
            foreach(var data in bothList)
            {
                bothAttackActionDataDic.Add(data.attackActionName, data);
            }

            currentDic = weaponManager.isBothHand? bothAttackActionDataDic:singleAttackActionDataDic;
        }

        public void ClearAttack()
        {
            if(nextAttackAction==null)
            {
                currentAttackAction = null;
            }
            //weaponManager.currentAttackWeapon.CloseCollider();
            hitTargetCountDic.Clear();
            //weaponManager.currentAttackWeapon = null;
        }

        public void SetCurrentAttackAction()
        {
            if (nextAttackAction != null)
            {
                currentAttackAction = nextAttackAction;
                nextAttackAction = null;
                startAttackDir = character.model.forward;
                currentHitLevel = currentAttackAction.hitLevel + weaponManager.currentAttackWeapon.weaponType.GetHashCode();
                return;
            }
        }

        public void GetNextAttack()
        {
            if (nextAttackAction != null)
                return;
            
            AttackActionData bestActionData = null;
            int priorityLevel = -1;
            foreach (var attackName in currentAttackAction.nextAttackActionNames)
            {
                bool canAttack = true;
                AttackActionData attackActionData = currentDic[attackName];
                foreach (var key in currentDic[attackName].inputBufferKeys)
                {
                    if (!character.input.inputBufferDic[key].ReadKey())
                    {
                        canAttack = false;
                        break;
                    }
                }

                if (canAttack && priorityLevel < attackActionData.priorityLevel)
                {
                    bestActionData = attackActionData;
                    priorityLevel = attackActionData.priorityLevel;
                }
            }

            nextAttackAction = bestActionData;
        }

        public void GetFirstAttack()
        {
            if (nextAttackAction != null)
                return;
            AttackActionData bestActionData=null;
            int priorityLevel = -1;
            foreach(var attack in currentDic)
            {
                AttackActionData attackActionData = attack.Value;
                if(attackActionData.canFirstAttack)
                {
                    bool canAttack = true;
                    foreach(var key in attackActionData.inputBufferKeys)
                    {
                        if(!character.input.inputBufferDic[key].ReadKey())
                        {
                            canAttack = false;
                            break;
                        }
                    }

                    if(canAttack&&priorityLevel<attackActionData.priorityLevel)
                    {
                        bestActionData = attackActionData;
                        priorityLevel = attackActionData.priorityLevel;                    
                    }
                }
            }

            nextAttackAction = bestActionData;
        }
    }
}

