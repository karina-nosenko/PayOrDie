using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleType", menuName = "Bubble Type", order = 1)]
public class BubbleType : ScriptableObject
{
    public string bubbleName;
    public Color bubbleColor;
    public float growthRate;
    public float gravitationalForce;
    public float maxRadius;

    public float value;  // This is the value we want to display on the bubble
    public Sprite bubbleImage;  // The PNG image for the bubble
}
