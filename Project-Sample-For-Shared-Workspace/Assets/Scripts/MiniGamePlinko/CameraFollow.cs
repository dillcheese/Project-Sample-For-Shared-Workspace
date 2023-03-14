using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    private bool delayComplete = false;

    private void Update()
    {
        if (!delayComplete)
        {
            // Wait for one second
            Invoke("CompleteDelay", 1f);
        }
    }

    private void LateUpdate()
    {
        if (delayComplete)
        {
            // Calculate the desired position of the camera
            Vector3 targetPosition = transform.position;
            targetPosition.y = objectToFollow.transform.position.y;

            // Smoothly move the camera to the target position over time
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }

    private void CompleteDelay()
    {
        delayComplete = true;
        Debug.Log("Camera delay complete!");
    }
}