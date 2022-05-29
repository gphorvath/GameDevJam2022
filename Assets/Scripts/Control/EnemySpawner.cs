using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;


        [SerializeField] private float spawnInterval = 3.5f;
        private Vector2 location;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
            location = transform.position;
        }

        private IEnumerator spawnEnemy(float interval, GameObject enemy)
        {
            yield return new WaitForSeconds(interval);

            GameObject newenemy = Instantiate(enemy, new Vector2(location.x, location.y), Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    }
}