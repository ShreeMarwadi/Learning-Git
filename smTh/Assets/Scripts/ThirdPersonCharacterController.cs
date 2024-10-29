using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;            // Movement speed of the character
    public float rotationSpeed = 10f;       // Rotation speed of the character in degrees per second

    private CharacterController characterController;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate input direction based on input values
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if there is any input
        if (inputDirection.magnitude >= 0.1f)
        {
            // Smoothly rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the character in the direction they are facing
            characterController.Move(transform.forward * moveSpeed * Time.deltaTime);
        }
    }
}
