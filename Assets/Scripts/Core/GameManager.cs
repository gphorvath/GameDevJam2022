using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Stats;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int enemiesAlive {get; private set;} = 0;

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

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void FixedUpdate() 
        {
            enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        public void LoadSceneByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Find player in the newly loaded scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // If player is found, subscribe to its OnDeath event
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.OnDeath += LoadDeathScene;
                }
            }
        }

        private void LoadDeathScene()
        {
            if (SceneManager.GetActiveScene().name == "Overworld")
            {
                // Here we assume that you have a scene named "DeathScene" in your project that you want to load
                SceneManager.LoadScene("DeathScene");
            }
            else if (SceneManager.GetActiveScene().name == "Underworld")
            {
                // Here we assume that you have a scene named "DeathScene" in your project that you want to load
                SceneManager.LoadScene("LifeScene");
            }
        }

        private void OnDestroy()
        {
            // It's good practice to unsubscribe from events when the object is destroyed
            SceneManager.sceneLoaded -= OnSceneLoaded;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.OnDeath -= LoadDeathScene;
                }
            }
        }
    }
}