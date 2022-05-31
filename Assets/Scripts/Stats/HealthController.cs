using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace RPG.Stats
{
    public class HealthController : MonoBehaviour
    {

        [field: SerializeField] private int maxHealth = 10;

        private int currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(int damage)
        {
            currentHealth = Math.Max(currentHealth - damage, 0);

            if (currentHealth <= 0)
            {
                Debug.Log("You Died!");
            }
        }
    }
}