using JetBrains.Annotations;
using NUnit.Framework;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<Order> easyOrders;
    public List<Order> mediumOrders;
    public List<Order> hardOrders;



    public UDictionary<int, int> diasPorDificultad;
    public int currentDay = 0;



    public Order currentOrder;

    public string orderDescription;
    public UDictionary<FlavourType, int> orderFlavourType;
    public List<Sprite> customerSprites;
    public Sprite currentCustomerSprite;


    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateDay()
    {

        if (currentDay <= 2)
            GenerateCustomer(0);
        else if (currentDay <= 4)
            GenerateCustomer(1);
        else
            GenerateCustomer(2);

    }

    private void GenerateCustomer(int difficulty)
    {
        int choice = 0;
        if(difficulty == 0)
        {
            choice = Random.Range(0, easyOrders.Count);
            currentOrder = easyOrders[choice];
        }

        if (difficulty == 1)
        {
            choice = Random.Range(0, mediumOrders.Count);
            currentOrder = mediumOrders[choice];
        }

        if (difficulty == 2)
        {
            choice = Random.Range(0, hardOrders.Count);
            currentOrder = hardOrders[choice];
        }

        //Hacer que se muestre el texto en una burbuja
        orderDescription = currentOrder.description;

        //Me guardo el flavoyr type para que luego sea el que pides
        orderFlavourType = currentOrder.flavours;


        //Poner el sprite customer
        int count = Random.Range(0, hardOrders.Count);
        currentCustomerSprite = customerSprites[count];
        GetComponent<SpriteRenderer>().sprite = currentCustomerSprite;
    }

    public bool ReciveBubba(UDictionary<FlavourType, int> bubbaFlavours)
    {
        bool correctOrder = false;

        correctOrder = CompareX(orderFlavourType, bubbaFlavours);

       return correctOrder;
    }



    //en principio esto compara ambos diccionarios y devuelve si son iguales, si lo son devuelve true, si no, false
    public bool CompareX<TKey, TValue>(
    UDictionary<TKey, TValue> dict1, UDictionary<TKey, TValue> dict2)
    {
        if (dict1 == dict2) return true;
        if ((dict1 == null) || (dict2 == null)) return false;
        if (dict1.Count != dict2.Count) return false;

        var valueComparer = EqualityComparer<TValue>.Default;

        foreach (var kvp in dict1)
        {
            TValue value2;
            if (!dict2.TryGetValue(kvp.Key, out value2)) return false;
            if (!valueComparer.Equals(kvp.Value, value2)) return false;
        }
        return true;
    }

}
