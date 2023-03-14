using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public GameObject objectToFollow;

    public Transform objectToFollow;

    private bool delayComplete = false;
    public float followSpeed = 5f;
    public float minYPosition = 0.25f;
    public float delay = 2f;
    private bool isCameraFocused = false;


    private void Start()
    {
        delayComplete = false;
    }

    private void Update()
    {
        if (!delayComplete)
        {
            // Wait for one second
            Invoke("CompleteDelay", delay);
        }
        if (objectToFollow != null && delayComplete)
        {
            // Calculate the desired Y position of the camera
            float targetYPosition = objectToFollow.position.y;

            // Limit the Y position of the camera
            targetYPosition = Mathf.Max(targetYPosition, minYPosition);

            // Calculate the current position of the camera
            Vector3 currentPosition = transform.position;

            // Set the X and Z positions of the current position to the target Y position
            currentPosition.y = targetYPosition;

            // Move the camera towards the current position over time
            transform.position = Vector3.MoveTowards(transform.position, currentPosition, followSpeed * Time.deltaTime);


            // If the camera has reached the target position, set the isCameraFocused variable to true
            if (transform.position == currentPosition)
            {
                isCameraFocused = true;
            }
        }
    }

    private void CompleteDelay()
    {
        delayComplete = true;
        //Debug.Log("Camera delay complete!");
    }

    // Getter for the isCameraFocused variable
    public bool IsCameraFocused()
    {
        return isCameraFocused;
    }
}