using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class PlayerStateManager : StateManager
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ChangeHealth(-10,false);
            }

            //ChangeHealth(2 * Time.deltaTime, true);
        }

        public override void ChangeEndurance(float changeValue, bool isSmooth)
        {
            float targetValue = currentEndurance + changeValue;

            targetValue = Mathf.Clamp(targetValue, 0, maxEndurance);

            changeValue = targetValue - currentEndurance;
            if (changeValue == 0)
                return;

            

            if (isSmooth)
                EventCenter.Instance.EventTrigger("EnduranceSmoothChange", changeValue);
            else
                EventCenter.Instance.EventTrigger("EnduranceNoSmoothChange", changeValue);
            currentEndurance = targetValue;
        }

        public override void ChangeHealth(float changeValue, bool isSmooth)
        {
            float targetValue = currentHealth + changeValue;

            targetValue = Mathf.Clamp(targetValue, 0, maxHealth);

            changeValue = targetValue - currentHealth;
            if (changeValue == 0)
                return;

            if (isSmooth)
                EventCenter.Instance.EventTrigger("HealthSmoothChange", changeValue);
            else
                EventCenter.Instance.EventTrigger("HealthNoSmoothChange", changeValue);
            currentHealth = targetValue;
        }

        public override void Init(CharacterController character)
        {
            base.Init(character);
            //Debug.Log("UI");
            //EventCenter.Instance.EventTrigger("SetMaxEndurance", maxEndurance);
            //EventCenter.Instance.EventTrigger("SetMaxHealth", maxHealth);           
        }
    }

}

