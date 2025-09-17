using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

public class playerMovement : MonoBehaviour
{
    public float speed = 5; // Movement speed multiplier
    public Rigidbody2D rb;  // Reference to the Rigidbody2D component

    Vector2 movement; // Stores the current movement direction

    // Update reads input every frame and stores the direction in 'movement'
    void Update()
    {
        // Check if a keyboard is connected (new Input System)
        if (Keyboard.current != null)
        {
            float horizontal = 0f;
            float vertical = 0f;

            // Start at 0, then adjust based on WASD keys:
            // A/D modify horizontal, W/S modify vertical
            // Example: Pressing D sets horizontal to 1, pressing A sets it to -1
            //          Pressing W sets vertical to 1, pressing S sets it to -1
            //          Diagonals combine (e.g., W+D = (1,1))
            if (Keyboard.current.aKey.isPressed) horizontal -= 1f;
            if (Keyboard.current.dKey.isPressed) horizontal += 1f;
            if (Keyboard.current.sKey.isPressed) vertical -= 1f;
            if (Keyboard.current.wKey.isPressed) vertical += 1f;

            // Normalize to prevent faster diagonal movement
            movement = new Vector2(horizontal, vertical).normalized;
        }
    }

    // FixedUpdate applies the stored movement to the Rigidbody2D for smooth physics-based movement
    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D using linearVelocity (new API)
        rb.linearVelocity = movement * speed;
    }
}