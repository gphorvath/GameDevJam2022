using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;

namespace RPG.UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private Scrollbar _healthBar;

        private Health _playerHealth;

        public static UIManager instance;

        void Awake()
        {
            // Implementing the singleton pattern
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

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