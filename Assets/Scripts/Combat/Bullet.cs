using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using RPG.Util;

namespace RPG.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage = 10;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 1.1f;
        [SerializeField] private GameObject fireball;
        private Vector2 direction;
        private ObjectPool bulletPool;



        private void Awake()
        {
            // Get the object pool from the parent (optional, depends on your hierarchy)
            bulletPool = GetComponentInParent<ObjectPool>();
        }

        private void OnEnable()
        {
            // Start the lifetime countdown whenever the bullet is enabled
            StartCoroutine(LifeCountdown());
        }

        private void Update()
        {
            // Move the bullet forward at a constant speed
            transform.Translate(direction * speed * Time.deltaTime);

            // Rotate the sprite to face the direction of movement
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                fireball.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        public void SetDirection(Vector2 newDirection)
        {
            direction = newDirection;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the bullet hit an enemy
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Get the enemy's health component
                Health enemyHealth = other.gameObject.GetComponent<Health>();

                // If the enemy has a health component, damage it
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
                // Return the bullet to the pool
                bulletPool.ReturnObject(gameObject);
            }

            if (other.gameObject.CompareTag("Tree"))
            {
                // Return the bullet to the pool
                bulletPool.ReturnObject(gameObject);
            }
        }

        private IEnumerator LifeCountdown()
        {
            yield return new WaitForSeconds(lifeTime);

            // After the lifetime has passed, return the bullet to the pool
            bulletPool.ReturnObject(gameObject);
        }
    }
}