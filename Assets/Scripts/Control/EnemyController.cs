using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int damange = 5;
        [SerializeField] private float speed = 1.5f;

        [SerializeField] private EnemyData data;

        private GameObject player;

        private void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update() {
            Swarm();
        }


        private void Swarm()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

    }
}
