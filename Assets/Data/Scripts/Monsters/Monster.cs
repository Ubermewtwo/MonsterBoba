using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public string Name;
    public Sprite MonsterSprite;
    public Sprite EggSprite;
    public Color EggColor;
    public UDictionary<MonsterPart, int> MonsterParts;
}
