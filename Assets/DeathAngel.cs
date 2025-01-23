using UnityEngine;

public class DeathAngel : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is required on the Death Angel.");
        }
    }

    void FixedUpdate()
    {
        PullByBubbles();
    }

    private void PullByBubbles()
    {
        float dampingFactor = 0.9f;
        float minVelocityThreshold = 0.2f;
        // Find all bubbles in the scene
        Bubble[] bubbles = FindObjectsOfType<Bubble>();

        foreach (Bubble bubble in bubbles)
        {
            if (bubble != null && bubble.bubbleType != null)
            {
                // Calculate the direction from the Death Angel to the bubble
                Vector2 direction = (bubble.transform.position - transform.position).normalized;

                // Calculate the distance between the Death Angel and the bubble
                float distance = Vector2.Distance(transform.position, bubble.transform.position);
                float minDistance = 0.3f; // Threshold to consider as "close"
                // Ensure we have a valid distance to prevent division by zero
                if (distance > minDistance)
                {
                    // Gravitational pull should be proportional to gravitationalForce and inversely proportional to distance^2
                    float pullStrength = bubble.bubbleType.gravitationalForce / (distance * distance);

                    // Apply the force with respect to the gravitational strength and direction
                    rb.AddForce(direction * pullStrength);
                }
                else
                {
                    // Gradually reduce the velocity to simulate damping
                    rb.linearVelocity *= dampingFactor;

                    // Stop completely if velocity is very low
                    if (rb.linearVelocity.magnitude < minVelocityThreshold)
                    {
                        rb.linearVelocity = Vector2.zero; // Stabilize motion
                        rb.position = bubble.transform.position; // Snap to bubble
                    }
                    if(distance<0.01f){
                        Destroy(bubble.gameObject); 
                    }
                }
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the Death Angel collided with a bubble
        Bubble bubble = collision.gameObject.GetComponent<Bubble>();
        if (bubble != null)
        {
            // Trigger the bubble's collision event
            OnBubbleCollision(bubble);
        }
    }

    private void OnBubbleCollision(Bubble bubble)
    {
        // Example event logic: Destroy the bubble and log a message
        Debug.Log($"Death Angel collided with bubble: {bubble.bubbleType.bubbleName}");
        Destroy(bubble.gameObject);

        // You can implement additional event behavior here
    }
}
