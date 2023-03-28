using UnityEngine;

namespace MiniGamePlinko
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        public PhysicsMaterial2D _material;

        // Set the initial bounciness value
        [Tooltip("Set bounciness value of the physicsMaterial2D")]

        public float bounceFactor = 0.5f;

        [Tooltip("Set the force the ball bounces by when it hits an obstacle")]
        //the force the ball bounces by when it hits an obstacle
        public float obstacleBounceFactor = 0.1f;

        // public bool isCameraFocused = false; // added variable
        private CameraFollow cameraFollow;

      //  public GameObject area;
       // private RectTransform rectTransform;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
            rb.Sleep();
            cameraFollow = Camera.main.GetComponent<CameraFollow>();
        }

        private void Update()
        {

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            if (Input.GetMouseButtonDown(0) && rb.IsSleeping() && cameraFollow.IsCameraFocused())
            {
                rb.WakeUp();

                //get touch
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                // Set the ball's position to the touch coordinates
                transform.position = new Vector2(touchPosition.x, transform.position.y);
            }


            if (Input.touchCount == 1 && rb.IsSleeping() && cameraFollow.IsCameraFocused())
            {
                rb.WakeUp();

                //get touch
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                
                // Set the ball's position to the touch coordinates
                transform.position = new Vector2(touchPosition.x, transform.position.y);
                
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