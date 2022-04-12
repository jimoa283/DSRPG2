using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class StateManager : MonoBehaviour
    {
        public CharacterController character;
        public bool isBeCounterBack;
        public float maxHealth;
        public float maxEndurance;
        public float currentHealth;
        public float currentEndurance;
        public virtual void Init(CharacterController character)
        {
            this.character = character;
            currentHealth = maxHealth;
            currentEndurance = maxEndurance;
            /*EventCenter.Instance.EventTrigger("SetMaxHealth", maxHealth);
            EventCenter.Instance.EventTrigger("SetMaxEndurance", maxEndurance);*/
        }

/*        private void Update()
        {
           *//* ChangeHealth(Time.deltaTime * 2, true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeHealth(-10, false);
            }    *//*
        }
*/
        public virtual void ChangeHealth(float changeValue,bool isSmooth)
        {
           /* float targetValue = currentHealth + changeValue;

            targetValue = Mathf.Clamp(targetValue, 0, maxHealth);

            if (isSmooth)
                EventCenter.Instance.EventTrigger("HealthSmoothChange", targetValue - currentHealth);
            else
                EventCenter.Instance.EventTrigger("HealthNoSmoothChange", targetValue - currentHealth);
            currentHealth = targetValue;*/
        }

        public virtual void ChangeEndurance(float changeValue, bool isSmooth)
        {
           /* float targetValue = currentEndurance + changeValue;

            targetValue = Mathf.Clamp(targetValue, 0, maxEndurance);

            if (isSmooth)
                EventCenter.Instance.EventTrigger("EnduranceSmoothChange", targetValue - currentEndurance);
            else
                EventCenter.Instance.EventTrigger("EnduranceNoSmoothChange", targetValue - currentEndurance);
            currentEndurance = targetValue;*/
        }
    }

}
