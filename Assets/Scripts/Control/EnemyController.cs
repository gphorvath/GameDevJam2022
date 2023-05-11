using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField] public float speed { get; private set; } = 1.5f;
        [field: SerializeField] public float chaseDistance { get; private set; } = 7f;
        [field: SerializeField] public float wanderRadius { get; private set; } = 5f;
        [field: SerializeField] public float alertDistance { get; private set; } = 9f;

        [SerializeField] private EnemyData data;

        private Transform target;
        private Vector3 wanderTarget;
        private bool directlyAlerted = false;


        private enum State { Idle, Wander, Chase }
        private State state;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target == null)
            {
                Debug.LogError("Player object not found");
                return;
            }
            state = State.Idle;
            StartCoroutine(FSM());
        }


        private IEnumerator FSM()
        {
            while (true)
            {
                switch (state)
                {
                    case State.Idle:
                        yield return StartCoroutine(Idle());
                        break;
                    case State.Wander:
                        yield return StartCoroutine(Wander());
                        break;
                    case State.Chase:
                        yield return StartCoroutine(Chase());
                        break;
                }
            }
        }

        private IEnumerator Idle()
        {
            while (state == State.Idle)
            {
                yield return new WaitForSeconds(1f);

                if (Vector3.Distance(target.position, transform.position) < chaseDistance)
                {
                    state = State.Chase;
                    directlyAlerted = true;  // Set the flag when the enemy starts chasing due to being close to the player
                }
                else if (IsChasingEnemyNearby())
                    state = State.Chase;
                else
                    state = State.Wander;
            }
        }

        private IEnumerator Wander()
        {
            float wanderTime = Random.Range(1f, 5f); // wander for a random duration between 1 and 5 seconds
            Vector3 wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized; // pick a random direction

            float timer = 0;
            while (state == State.Wander)
            {
                timer += Time.deltaTime;
                if (timer >= wanderTime)
                {
                    // Time to pick a new direction and duration
                    wanderTime = Random.Range(1f, 5f);
                    wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
                    timer = 0;
                }

                // Move in the chosen direction
                Vector3 targetPosition = transform.position + wanderDirection * wanderRadius;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (Vector3.Distance(target.position, transform.position) < chaseDistance)
                    state = State.Chase;

                yield return null;
            }
        }

        private IEnumerator Chase()
        {
            while (state == State.Chase)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (Vector3.Distance(target.position, transform.position) <= chaseDistance)
                {
                    directlyAlerted = true;  // Set the flag when the enemy is close to the player
                }

                if (Vector3.Distance(target.position, transform.position) > chaseDistance)
                {
                    state = State.Wander;
                    directlyAlerted = false;  // Reset the flag when the enemy stops chasing
                }

                AlertNearbyEnemies();

                yield return null;
            }
        }

        private void AlertNearbyEnemies()
        {
            if (!directlyAlerted)  // Only alert other enemies if directlyAlerted is true
                return;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, alertDistance);
            foreach (Collider2D collider in colliders)
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                if (enemyController != null && enemyController.state != State.Chase)
                    enemyController.state = State.Chase;
            }
        }


        private bool IsChasingEnemyNearby()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, alertDistance);
            foreach (Collider2D collider in colliders)
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                if (enemyController != null && enemyController.state == State.Chase)
                    return true;
            }
            return false;
        }

        // Add this to visualize chase distance and wander radius
        private void OnDrawGizmosSelected()
        {
            // Draw chase distance
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

            // Draw wander radius
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, wanderRadius);

            // Draw alert distance
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, alertDistance);
        }
    }
}
