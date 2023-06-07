using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;
using TMPro;
using RPG.Core;

namespace RPG.UI
{
    public class UIHUD : MonoBehaviour
    {

        [SerializeField] private Scrollbar _healthBar;
        [SerializeField] private TMP_Text _enemyCounter;

        private Health _playerHealth;

        // Start is called before the first frame update
        void Start()
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            _playerHealth.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(_playerHealth.GetMaxHealth());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate() 
        {
            UpdateEnemyCount(GameManager.instance.enemiesAlive);
        }

        void UpdateHealthBar(float currentHealth)
        {
            _healthBar.size = currentHealth / _playerHealth.GetMaxHealth();
        }

        void UpdateEnemyCount(int enemyCount)
        {
            _enemyCounter.text = enemyCount.ToString();
        }
    }
}