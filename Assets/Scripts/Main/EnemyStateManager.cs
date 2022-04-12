using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class EnemyStateManager : StateManager
    {
        public EnemyValueBar enemyHealthar;

        public override void ChangeHealth(float changeValue, bool isSmooth)
        {
            float targetValue = currentHealth + changeValue;

            targetValue = Mathf.Clamp(targetValue, 0, maxHealth);
            currentHealth = targetValue;

            if (isSmooth)
                enemyHealthar.SmoothAdd(targetValue - currentHealth);
            else
                enemyHealthar.NoSmoothAdd(targetValue - currentHealth);
            
        }

        public void ShowHealthBar()
        {
            if (enemyHealthar.gameObject.activeSelf)
                return;
            enemyHealthar.gameObject.SetActive(true);
            enemyHealthar.SetValue(currentHealth, maxHealth);
        }

        public override void Init(CharacterController character)
        {
            base.Init(character);
            enemyHealthar = GetComponentInChildren<EnemyValueBar>();
            enemyHealthar.Init();
            enemyHealthar.maxValue = maxHealth;        
        }

    }

}
