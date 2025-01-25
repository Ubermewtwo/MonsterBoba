using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Day", menuName = "Scriptable Objects/Day")]
public class Day : ScriptableObject
{
    public int day;
    public int numberOfCustomers;
    public float dayTimeInSeconds;
    public bool easyOrders;
    public bool mediumOrders;
    public bool hardOrders;
    

}
