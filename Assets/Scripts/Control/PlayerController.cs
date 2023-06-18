using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Util;
using RPG.Stats;
using RPG.Combat;


namespace RPG.Control
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Health))]
    public class PlayerController : MonoBehaviour
    {

        [field: SerializeField] public float moveSpeed { get; private set; } = 5f;

        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private Transform firePoint;

        private Vector2 movement;
        private Rigidbody2D _rb;
        private Health _health;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
            _health.OnDeath += this.OnDeath;
        }

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            movement.Normalize();

            if (Input.GetButtonDown("Fire1"))
            {
                FireBullet();
            }
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }


        private void FireBullet()
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            // Calculate the direction to the mouse position
            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            // Normalize the direction
            direction.Normalize();

            // Get a bullet from the pool and set its position
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = firePoint.position;

            // Set the bullet's direction
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }

        private void OnDeath()
        {

        }
    }
}
