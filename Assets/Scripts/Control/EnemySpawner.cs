using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;


        [SerializeField] private float spawnInterval = 3.5f;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        }

        private IEnumerator spawnEnemy(float interval, GameObject enemy)
        {
            yield return new WaitForSeconds(interval);

            GameObject newenemy = Instantiate(enemy, new Vector2(Random.Range(-5f,5), Random.Range(-5f, 5f)), Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }
}