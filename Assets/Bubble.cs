using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public BubbleType bubbleType;

    private Transform bubbleTransform;
    private CircleCollider2D gravityField;
    private SpriteRenderer spriteRenderer;
    private GameObject imageObject;
    private TextMeshProUGUI valueText;
    private Canvas bubbleCanvas;
    private RectTransform canvasRect; // Add this

    void Start()
    {
        bubbleTransform = transform;
        gravityField = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyBubbleType();
        CreateBubbleImage();
        CreateValueText();
    }

    void Update()
    {
        if (bubbleTransform.localScale.x < bubbleType.maxRadius)
        {
            float growth = bubbleType.growthRate * Time.deltaTime;
            bubbleTransform.localScale += new Vector3(growth, growth, 0);
            gravityField.radius = bubbleTransform.localScale.x / 2;

            if (imageObject != null)
            {
                imageObject.transform.localScale = Vector3.one / bubbleTransform.localScale.x;
            }

            // Adjust Canvas size based on bubble scale. No need to scale the canvas
            if (bubbleCanvas != null)
            {
                canvasRect.sizeDelta = bubbleTransform.localScale * 100; // Important: Scale the rect
            }
        }

        if (valueText != null && bubbleType != null)
        {
            valueText.text = "₪" + bubbleType.value.ToString("F0");
        }
    }


    private void ApplyBubbleType()
    {
        if (spriteRenderer != null)
        {
            Color bubbleColor = bubbleType.bubbleColor;
            bubbleColor.a = 1f;
            spriteRenderer.color = bubbleColor;
        }
    }

    private void CreateBubbleImage()
    {
        if (bubbleType.bubbleImage != null)
        {
            imageObject = new GameObject("BubbleImage");
            imageObject.transform.SetParent(transform);
            imageObject.transform.localPosition = Vector3.zero;

            SpriteRenderer imageRenderer = imageObject.AddComponent<SpriteRenderer>();
            imageRenderer.sprite = bubbleType.bubbleImage;
            imageRenderer.sortingOrder = spriteRenderer.sortingOrder + 1; 
        }
    }

    private void CreateValueText()
    {
        // Create a Canvas for the bubble
        bubbleCanvas = new GameObject("BubbleCanvas").AddComponent<Canvas>();
        bubbleCanvas.renderMode = RenderMode.WorldSpace; // Use WorldSpace for scaling
        bubbleCanvas.transform.SetParent(transform);
        bubbleCanvas.transform.localPosition = Vector3.zero;

        canvasRect = bubbleCanvas.GetComponent<RectTransform>(); // Get the RectTransform
        canvasRect.sizeDelta = Vector2.one * 100; // Initial size

        // Create the value text object as a child of the canvas
        GameObject valueTextObject = new GameObject("ValueText");
        valueTextObject.transform.SetParent(bubbleCanvas.transform);
        valueTextObject.transform.localPosition = Vector3.zero; // Center the text

        valueText = valueTextObject.AddComponent<TextMeshProUGUI>();
        valueText.text = "₪" + bubbleType.value.ToString("F0");
        valueText.fontSize = 14;
        valueText.color = Color.white;
        valueText.alignment = TextAlignmentOptions.Center; //Center the text

        // Crucial Fixes:
        RectTransform textRect = valueText.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(100, 50); // Set a reasonable size
        textRect.localScale = Vector3.one; // Ensure no local scaling
    }
}