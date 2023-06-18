using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {

        [field: SerializeField]
        public float moveSpeed { get; private set; } = 5f;
        Vector2 movement;
        private Rigidbody2D theRB;

        // Start is called before the first frame update
        void Start()
        {
            theRB = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            theRB.MovePosition(theRB.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
