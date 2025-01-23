using TMPro;  // Ensure the TextMeshPro namespace is included
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public BubbleType bubbleType;  // Reference to the ScriptableObject (BubbleType)

    private Transform bubbleTransform;
    private CircleCollider2D gravityField;
    private SpriteRenderer spriteRenderer; // Main circle's sprite renderer
    private GameObject imageObject;       // Child object for the image
    private TextMeshProUGUI valueText;  // Reference to the dynamically created value text object
    private Canvas bubbleCanvas; // Reference to the Canvas for the UI

    void Start()
    {
        bubbleTransform = transform;
        gravityField = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Apply bubble type properties
        ApplyBubbleType();

        // Create and attach the image to the bubble
        CreateBubbleImage();

        // Create and display the value text dynamically
        CreateValueText();
    }

    void Update()
    {
        // Gradually grow the bubble
        if (bubbleTransform.localScale.x < bubbleType.maxRadius)
        {
            float growth = bubbleType.growthRate * Time.deltaTime;
            bubbleTransform.localScale += new Vector3(growth, growth, 0);

            // Adjust gravitational field size
            gravityField.radius = bubbleTransform.localScale.x / 2;

            // Ensure the image stays centered and scales correctly
            if (imageObject != null)
            {
                imageObject.transform.localScale = Vector3.one / bubbleTransform.localScale.x; // Keep consistent size
            }
        }

        // Update the value text with the value from BubbleType
        if (valueText != null && bubbleType != null)
        {
            valueText.text = "₪" + bubbleType.value.ToString("F0"); // Display the value as a whole number with the currency symbol
        }
    }

    private void ApplyBubbleType()
    {
        // Set color of the main circle's sprite
        if (spriteRenderer != null)
        {
            Color bubbleColor = bubbleType.bubbleColor;
            bubbleColor.a = 1f; // Ensure the alpha is fully opaque
            spriteRenderer.color = bubbleColor;
        }
    }

    private void CreateBubbleImage()
    {
        // Create a child object for the image
        if (bubbleType.bubbleImage != null)
        {
            imageObject = new GameObject("BubbleImage");
            imageObject.transform.SetParent(transform);
            imageObject.transform.localPosition = Vector3.zero; // Center the image

            SpriteRenderer imageRenderer = imageObject.AddComponent<SpriteRenderer>();
            imageRenderer.sprite = bubbleType.bubbleImage;
            imageRenderer.sortingOrder = spriteRenderer.sortingOrder + 1; // Ensure the image appears above the bubble
        }
    }

    private void CreateValueText()
    {
        // NOT WORKING SHIT

        // GameObject valueTextObject = new GameObject("ValueText");
        // valueTextObject.transform.localPosition = bubbleTransform.position; // Start with the default position

        // valueText = valueTextObject.AddComponent<TextMeshProUGUI>();
        // valueText.text = "₪" + bubbleType.value.ToString("F0"); // Display the value with the currency symbol
        // valueText.fontSize = 14; // Set font size (adjust to your needs)
        // valueText.sortingOrder = spriteRenderer.sortingOrder + 2;

        // valueText.color = Color.white;
    }
}
