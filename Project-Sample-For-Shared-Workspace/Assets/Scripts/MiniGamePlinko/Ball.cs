using UnityEngine;

namespace MiniGamePlinko
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        public PhysicsMaterial2D _material;

        // Set the initial bounciness value
        public float bounceFactor = 0.5f;

        //the force the ball bounces by when initially being dropped
        public float wakeUpBounceFactor = 0.8f;

        //the force the ball bounces by when it hits an obstacle
        public float obstacleBounceFactor = 0.1f;

        // public bool isCameraFocused = false; // added variable
        private CameraFollow cameraFollow;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
            rb.Sleep();
            cameraFollow = Camera.main.GetComponent<CameraFollow>();
        }

        private void Update()
        {
            if (Input.touchCount == 1 && rb.IsSleeping() && cameraFollow.IsCameraFocused())
            {
                rb.WakeUp();
                // Apply a force to the ball in a random direction

                // Get the position of the touch input and the ball
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 ballPosition = transform.position;//pos of the ball

                // Calculate the drop direction on the x-axis clamped between 2
                float xDirection = Mathf.Clamp(touchPosition.x - ballPosition.x, -2f, 2f);

                // Set the minimum force to 1
                float force = Mathf.Max(Mathf.Abs(xDirection), 1.0f);

                //get dropdirection, y is randomized
                Vector2 dropDirection = new Vector2(xDirection, Random.Range(0.75f, 1f)).normalized;

                rb.AddForce(dropDirection * force * wakeUpBounceFactor, ForceMode2D.Impulse);
            }

            if (!rb.IsSleeping())
            {
                Mathf.Clamp(bounceFactor, 0f, 1f);
                _material.bounciness = bounceFactor;
                cd.sharedMaterial = _material;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Calculate the bounce direction
            Vector2 incomingDirection = rb.velocity.normalized;
            Vector2 normal = collision.contacts[0].normal;
            Vector2 bounceDirection = Vector2.Reflect(incomingDirection, normal);

            // Add a random angle to the bounce direction
            float angle = Random.Range(-45, 45);
            bounceDirection = Quaternion.Euler(0, 0, angle) * bounceDirection;

            // Apply the bounce force
            rb.AddForce(bounceDirection * obstacleBounceFactor, ForceMode2D.Impulse);
        }

        //false means it has woken up due to a tap
        public bool GetRbStatus()
        {
            return rb.IsSleeping();
        }
    }
}