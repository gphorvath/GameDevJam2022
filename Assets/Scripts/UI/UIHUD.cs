using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class UIHUD : MonoBehaviour
    {

        [SerializeField] private Scrollbar _healthBar;

        private Health _playerHealth;

        // Start is called before the first frame update
        void Start()
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            _playerHealth.OnHealthChanged += UpdateHealthBar;
            _healthBar.size = _playerHealth.GetMaxHealth() / _playerHealth.GetMaxHealth();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdateHealthBar(float currentHealth)
        {
            _healthBar.size = currentHealth / _playerHealth.GetMaxHealth();
        }
    }
}