using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterPart", menuName = "Scriptable Objects/MonsterPart")]
public class MonsterPart : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public UDictionary<FlavourType, int> Flavours;
    public BobaTeaPart BobaTeaPart;
    public AudioClipList AddedToBobaSound;
    public GameObject clickVFX;
}
