using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5; // Movement speed multiplier
    public Rigidbody2D rb;  // Reference to the Rigidbody2D component

    public Animator anim; // Reference to the Animator component for controlling animations

    public int facingDirection = 1;

    Vector2 movement; // Stores the current movement direction

    void Awake()
    {
        // Auto-assign if not set in Inspector
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

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

            // Only update animator if assigned
            if (anim != null)
            {
                anim.SetFloat("horizontal", Mathf.Abs(horizontal));
                anim.SetFloat("vertical", Mathf.Abs(vertical));
            }

            // Flip only if moving horizontally and direction changes
            if (horizontal != 0 && 
                ((horizontal > 0 && transform.localScale.x < 0) ||
                 (horizontal < 0 && transform.localScale.x > 0)))
            {
                Flip();
            }

            // Normalize to prevent faster diagonal movement
            movement = new Vector2(horizontal, vertical).normalized;
        }

        // Set the "speed" parameter in the Animator to the current movement magnitude
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            anim.SetFloat("speed", movement.magnitude);
        }
    }

    // FixedUpdate applies the stored movement to the Rigidbody2D for smooth physics-based movement
    void FixedUpdate()
    {
        // Null check for safety
        if (rb != null)
            rb.linearVelocity = movement * speed;


    }

    void Flip()
    {
        facingDirection *= -1;
    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}