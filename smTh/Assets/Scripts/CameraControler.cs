using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform target;                // Reference to the player or object to follow
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset position of the camera relative to the player
    public float mouseSensitivity = 2f;     // Sensitivity of mouse input
    public float distance = 5f;             // Distance the camera will maintain from the player
    public float smoothSpeed = 0.125f;      // Smoothing speed for camera movement

    private float yaw = 0f;                 // Horizontal rotation around the player
    private float pitch = 0f;               // Vertical rotation around the player
    private float minPitch = -30f;          // Minimum pitch angle (downward rotation)
    private float maxPitch = 60f;           // Maximum pitch angle (upward rotation)

    private void Start()
    {
        // Start with the initial rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        RotateCamera();
        FollowPlayer();
    }

    private void RotateCamera()
    {
        // Get mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update yaw and pitch based on mouse input
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp the pitch to prevent over-rotation
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Apply rotation to the camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position - (rotation * Vector3.forward * distance) + offset;
        transform.LookAt(target.position + Vector3.up * offset.y);
    }

    private void FollowPlayer()
    {
        // Smoothly move the camera to follow the player's position
        Vector3 desiredPosition = target.position - transform.forward * distance + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
