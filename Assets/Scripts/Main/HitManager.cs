using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class HitManager : MonoBehaviour
    {
        public CharacterController character;
        public List<string> targetTagList;
        public float defenseAngle;
        public int toughnessValue; //韧性

        public bool canCounterBack;

        public void Init(CharacterController character)
        {
            this.character = character;
        }

       /* private void OnTriggerEnter(Collider col)
        {
            if (!col.CompareTag("Weapon"))
                  return;

            GameObject attackerObj = col.transform.root.gameObject;

            if (!targetTagList.Contains(attackerObj.tag))
                   return;

            CharacterController attacker = attackerObj.GetComponent<CharacterController>();      

            Hit(attacker,HitDirectionCalculate(col.transform));
        }*/

        public HitDirection HitDirectionCalculate(Transform t)
        {
            Vector3 dir = (t.position - transform.position).normalized;
            dir.y = 0;
            if ((dir.y >= dir.x && dir.x >= 0) || (dir.y > -dir.x && dir.x < 0))
                return HitDirection.Forward;
            else if ((dir.y < dir.x && dir.x >= 0) || (dir.y >= -dir.x && dir.x > 0))
                return HitDirection.Right;
            else if ((dir.y < -dir.x && dir.x >= 0) || (dir.y <= dir.x && dir.x < 0))
                return HitDirection.Back;
            else
                return HitDirection.Left;
        }

        public bool DefenseValid(Transform attacker,Transform selfModel)
        {
            Vector3 countDir = attacker.position - selfModel.position;

            float counterAngle1 = Vector3.Angle(selfModel.forward, countDir);
            float counterAngle2 = Vector3.Angle(attacker.transform.forward, selfModel.transform.forward);

            return counterAngle1 < defenseAngle && Mathf.Abs(counterAngle2 - 180) > defenseAngle;
        }

        

        public void Hit(CharacterController attacker,Transform weaponTransform)
        {
            int totalHitLevel = attacker.attackManager.currentHitLevel - toughnessValue>0?
                attacker.attackManager.currentHitLevel-toughnessValue:0;

            bool defenseValid = DefenseValid(attacker.model, character.model);

            if (character.weaponManager.isShield&&defenseValid)
            {
                character.animatorManager.anim.SetFloat(AnimatorParam.HitLevel.ToString(), totalHitLevel > 2 ? 2 : 1);
                character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.Hit.GetHashCode());
            }
            else if(totalHitLevel>0)
            {
                HitDirection hitDirection = HitDirectionCalculate(weaponTransform);
                character.animatorManager.anim.SetFloat(AnimatorParam.HitLevel.ToString(), totalHitLevel);
                character.animatorManager.anim.SetFloat(AnimatorParam.HitDirection.ToString(), hitDirection.GetHashCode());
                character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.Hit.GetHashCode());
            }
        }
    }
}

