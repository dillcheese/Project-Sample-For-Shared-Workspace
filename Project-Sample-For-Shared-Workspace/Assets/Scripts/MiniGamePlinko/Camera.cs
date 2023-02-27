using UnityEngine;

namespace MiniGamePlinko
{
    public class Camera : MonoBehaviour
    {
        public Transform target; // The player's transform
        public float smoothSpeed = 0.125f; // The speed at which the camera follows the player
        public Vector3 offset; // The offset of the camera from the player

        private void Start()
        {
            // Calculate the initial offset between the camera and player
            offset = transform.position - target.position;
        }

        private void FixedUpdate()
        {
            //// Calculate the target position for the camera to move to
            //Vector3 targetPosition = target.position + offset;

            //// Move the camera towards the target position using Lerp for smoothness
            //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);

            //this.transform.position = new Vector3(target.position.x - (offset.x / 2f), target.position.y - (offset.y / 4f), this.transform.position.z);

            if (this.transform.position.y <= -0.5f)
            {
                this.transform.position = new Vector3(transform.position.x, -0.5f, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, target.position.y, this.transform.position.z);
            }
        }
    }
}