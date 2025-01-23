using UnityEngine;

[CreateAssetMenu(fileName = "PersonType", menuName = "Person Type")]
public class PersonType : ScriptableObject
{
    public string personName;
    public Color personColor;
    public Sprite personImage;
}
