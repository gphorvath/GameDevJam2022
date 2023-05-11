using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;


        [field: SerializeField] public float spawnInterval { get; private set; } = 3.5f;
        [field: SerializeField] public int maxSpawns { get; private set; } = 0;

        private Vector2 location;
        private int spawnCount = 0;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
            location = transform.position;
        }

        private IEnumerator spawnEnemy(float interval, GameObject enemy)
        {
            while (maxSpawns == 0 || spawnCount < maxSpawns)
            {
                yield return new WaitForSeconds(interval);

                Instantiate(enemyPrefab, location, Quaternion.identity);
                spawnCount++;
            }
        }
    }
}