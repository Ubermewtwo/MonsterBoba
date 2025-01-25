using JetBrains.Annotations;
using NUnit.Framework;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private List<Order> easyOrders;
    [SerializeField]
    private List<Order> mediumOrders;
    [SerializeField]
    private List<Order> hardOrders;

    private List<Order> currentDayOrders = new List<Order>();

    [SerializeField]
    private List<Day> days;
    private Day currentDay;
    private int currentDayCounter = 0;

    public UnityEvent<Day> OnDayChanged = new UnityEvent<Day>(); //evento>

    private Order currentOrder;

    private string orderDescription;
    public UDictionary<FlavourType, int> orderFlavourType;
    public List<Sprite> customerSprites;
    public Sprite currentCustomerSprite;

    private int currentDayDifficulty = 0;
    private int currentDayNumberOfCustomers = 0;
    public float currentDayTime = 0f;
    public bool hasDayEnded = false;

    [SerializeField]
    private CustomerDialog customerDialog;

    [SerializeField] private TextMeshProUGUI remainingTimeText; //texto
    [SerializeField] private Image remainingTimeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateDay();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("espacio pulsado");
            ReciveBubba(new UDictionary<FlavourType, int>());
        }

        if (hasDayEnded)
            return;

        currentDayTime -=Time.deltaTime;
        UpdateTimerText();

        if (currentDayTime <= 0)
        {
            currentDayTime = 0;
            EndDay();
        }


    }
    private void GenerateDay()
    {
        if (currentDayCounter >= days.Count)
        {
            //final del juego
            Debug.Log("Acabaste");
            return;
        }

        hasDayEnded = false;

        currentDay = days[currentDayCounter];
        currentDayNumberOfCustomers = currentDay.numberOfCustomers;
        currentDayTime = currentDay.dayTimeInSeconds;

        currentDayOrders.Clear();

        //Dependiendo de la dificultad del dia pone las ordenes que toquen
        if (currentDay.easyOrders)
            currentDayOrders.AddRange(easyOrders);
        if (currentDay.mediumOrders)
            currentDayOrders.AddRange(mediumOrders);
        if (currentDay.hardOrders)
            currentDayOrders.AddRange(hardOrders);

        currentDayCounter++;

        GenerateCustomer();
        OnDayChanged?.Invoke(currentDay);
    }

    private void GenerateCustomer()
    {

        /*
        if(currentDayNumberOfCustomers <= 0)
        {
            EndDay();
            return;
        }
        */
 
        int choice = choice = Random.Range(0, currentDayOrders.Count);
        currentOrder = currentDayOrders[choice];



        //Hacer que se muestre el texto en una burbuja
        orderDescription = currentOrder.description;
        //Debug.Log(currentOrder.description);

        //Me guardo el flavoyr type para que luego sea el que pides
        orderFlavourType = currentOrder.flavours;

        //Poner el sprite customer
        int count = Random.Range(0, customerSprites.Count);
        currentCustomerSprite = customerSprites[count];
        GetComponent<SpriteRenderer>().sprite = currentCustomerSprite;

        customerDialog.SetBubbleMessage(orderDescription);
        currentDayNumberOfCustomers--;
        

    }

    public bool ReciveBubba(UDictionary<FlavourType, int> bubbaFlavours)
    {
        bool correctOrder = false;

        correctOrder = CompareX(orderFlavourType, bubbaFlavours);
        //customerDialog.HideBubbleMessage();

        //esto habra que hacerlo despues, pero primero quiero unir las cosas
        GenerateCustomer();
        Debug.Log(correctOrder);

        return correctOrder;
    }

    public void EndDay()
    {
        //codigo de final de dia 

        hasDayEnded = true;

        //Esto realmente se llamara cuando acabe la parte de final de dia pero por ahora la pongo aqui para probar
        Debug.Log("Acaba el dia");
        GenerateDay();

    }

    public void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentDayTime / 60F);
        int seconds = Mathf.FloorToInt(currentDayTime - minutes * 60);

        string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
        remainingTimeText.text = timeText;
        remainingTimeImage.fillAmount = currentDayTime / currentDay.dayTimeInSeconds;
    }

    /* deprecated
    public void UpdateWaitingCustomersText()
    {
        waitingCustomersText.text = currentDayNumberOfCustomers.ToString();
    }
    */

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
