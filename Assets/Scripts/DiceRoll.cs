using UnityEngine;

[CreateAssetMenu(fileName = "DiceFace", menuName = "ScriptableObjects/DiceManagerScriptableObject")]
public class DiceRoll : ScriptableObject
{
    public int diceNumber;
    public Sprite OrangeDiceFace;
    public Sprite BlueDiceFace;
    public GameObject Prefab;
}
