using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class WeaponManager : MonoBehaviour
    {
        public CharacterController character;
        public WeaponData currentAttackWeapon;
        public WeaponData rightWeapon;
        public WeaponData leftWeapon;
        public InputBase input;
        public bool isBothHand;
        public bool isShield;
        public string targetTag;

        public void Init(CharacterController character)
        {
            this.character = character;
            currentAttackWeapon.Init(this);
            input = character.input;
            character.animatorManager.SetLocomotion(rightWeapon.locomotionType, isBothHand);
            character.animatorManager.SetAttack(rightWeapon.singleAttackActionDatas, rightWeapon.bothAttackActionDatas);
        }

        public void BothHandChange()
        {
            if (rightWeapon.canBothHand)
            {
                if(input.ReadKey(InputBufferKey.Y))
                {
                    isBothHand = !isBothHand;
                    character.animatorManager.anim.SetBool(AnimatorParam.BothHand.ToString(), isBothHand);                   
                    character.animatorManager.ChangeHandAnimation(isBothHand);
                }
            }
        }

        public void ChangeWeapon(WeaponData weaponData,bool isRight)
        {
            if (isRight)
            {
                rightWeapon = weaponData;
                if (isBothHand && !weaponData.canBothHand)
                    isBothHand = false;
                character.animatorManager.anim.SetBool(AnimatorParam.BothHand.ToString(), isBothHand);

                character.animatorManager.SetLocomotion(rightWeapon.locomotionType, isBothHand);
                character.animatorManager.SetAttack(rightWeapon.singleAttackActionDatas, rightWeapon.bothAttackActionDatas);
                character.animatorManager.ChangeHandAnimation(isBothHand);
            }
            else
            {
                if (isBothHand)
                    isBothHand = false;

                character.animatorManager.anim.SetBool(AnimatorParam.BothHand.ToString(), isBothHand);
                leftWeapon = weaponData;

            }
        }

        public void WeaponShow()
        {
            if(!isBothHand)
            {
                if (leftWeapon != null)
                    leftWeapon.gameObject.SetActive(true);

            }
            else
            {
                if (leftWeapon != null)
                    leftWeapon.gameObject.SetActive(false);
            }

            character.animatorManager.SetLocomotion(rightWeapon.locomotionType, isBothHand);
        }
    }
}

