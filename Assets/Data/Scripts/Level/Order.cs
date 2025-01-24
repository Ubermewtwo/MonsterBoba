using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Scriptable Objects/Order")]
public class Order : ScriptableObject
{
    public int difficulty = 0;
    public UDictionary<FlavourType, int> flavours;
    public string description;
}
