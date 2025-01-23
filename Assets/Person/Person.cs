using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour
{
    public PersonType personType;

    private Transform personTransform;
    private CircleCollider2D gravityField;
    private SpriteRenderer spriteRenderer;
    private GameObject imageObject;
    private TextMeshProUGUI valueText;
    private Canvas bubbleCanvas;
    private RectTransform canvasRect; // Add this

    void Start()
    {
        personTransform = transform;
        gravityField = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyPersonType();
        CreatePersonImage();
    }

    void Update()
    {
        
    }


    private void ApplyPersonType()
    {
        if (spriteRenderer != null)
        {
            Color personColor = personType.personColor;
            personColor.a = 1f;
            spriteRenderer.color = personColor;
        }
    }

    private void CreatePersonImage()
    {
        if (personType.personImage != null)
        {
            imageObject = new GameObject("PersonImage");
            imageObject.transform.SetParent(transform);
            imageObject.transform.localPosition = Vector3.zero;

            SpriteRenderer imageRenderer = imageObject.AddComponent<SpriteRenderer>();
            imageRenderer.sprite = personType.personImage;
            imageRenderer.sortingOrder = spriteRenderer.sortingOrder + 1; 
        }
    }
}