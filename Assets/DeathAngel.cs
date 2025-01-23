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
        KillPeople();
        CheckForWorldMapCollision();
    }
    private void CheckForWorldMapCollision()
    {
        // Get the RectTransform of the WorldMap Canvas
        RectTransform worldMapRect = GameObject.Find("WorldMap").GetComponent<RectTransform>();

        if (worldMapRect != null)
        {
            // Get the BoxCollider2D of the current Death Angel (same GameObject as this script)
            BoxCollider2D deathAngelCollider = GetComponent<BoxCollider2D>();

            // Check if the Death Angel's collider is outside the canvas bounds
            if (deathAngelCollider != null)
            {
                // WorldMap RectTransform's boundaries (in local space of the canvas)
                Vector3 minBounds = worldMapRect.TransformPoint(worldMapRect.rect.min);
                Vector3 maxBounds = worldMapRect.TransformPoint(worldMapRect.rect.max);

                // Get the current position of the Death Angel
                Vector3 deathAngelPos = transform.position;

                // If the Death Angel is outside the WorldMap bounds, flip its direction
                if (deathAngelPos.x < minBounds.x || deathAngelPos.x > maxBounds.x)
                {
                    rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y); // Flip X direction
                }
                if (deathAngelPos.y < minBounds.y || deathAngelPos.y > maxBounds.y)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y); // Flip Y direction
                }
            }
        }
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
                                          // Gravitational pull should be proportional to gravitationalForce and inversely proportional to distance^2
                float pullStrength = 0.1f + bubble.bubbleType.gravitationalForce / (distance * distance);

                // Apply the force with respect to the gravitational strength and direction
                rb.AddForce(direction * pullStrength);
                CircleCollider2D bubbleCollider = bubble.GetComponent<CircleCollider2D>();
                // Ensure the person's collider exists
                if (bubbleCollider != null)
                {
                    // Get the BoxCollider2D of the current Death Angel (same GameObject as this script)
                    BoxCollider2D deathAngelCollider = GetComponent<BoxCollider2D>();

                    // Check if the two BoxColliders are overlapping
                    if (bubbleCollider.bounds.Intersects(deathAngelCollider.bounds))
                    {
                        rb.AddForce(-20 * direction * pullStrength);
                        // Collision detected, handle it
                        DebtDisplay debtDisplay = FindObjectOfType<DebtDisplay>();
                        debtDisplay.AddValueToDebt(bubble.bubbleType.value);
                        Destroy(bubble.gameObject);
                    }
                }
            }
        }
    }

    private void KillPeople()
    {

        Person[] people = FindObjectsOfType<Person>();

        foreach (Person person in people)
        {
            Debug.Log(person);
            if (person != null)
            {
                // Get the BoxCollider2D of the current person
                BoxCollider2D personCollider = person.GetComponent<BoxCollider2D>();

                // Ensure the person's collider exists
                if (personCollider != null)
                {
                    // Get the BoxCollider2D of the current Death Angel (same GameObject as this script)
                    BoxCollider2D deathAngelCollider = GetComponent<BoxCollider2D>();

                    // Check if the two BoxColliders are overlapping
                    if (personCollider.bounds.Intersects(deathAngelCollider.bounds))
                    {
                        // Collision detected, handle it
                        DebtDisplay debtDisplay = FindObjectOfType<DebtDisplay>();
                        debtDisplay.AddValueToDebt(-200);
                        Destroy(person.gameObject); // Destroy the person on collision
                    }
                }
            }


        }
    }
}