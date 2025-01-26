using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class NPCVoices
{
    public AudioClipList OrderSounds;
    public AudioClipList CorrectOrderSounds;
    public AudioClipList WrongOrderSounds;
}

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
    [SerializeField] private List<NPCVoices> npcVoices;
    private Sprite currentCustomerSprite;
    private NPCVoices currentNPCVoices;

    private int currentDayDifficulty = 0;
    private int currentDayNumberOfCustomers = 0;
    public float currentDayTime = 0f;
    public bool hasDayEnded = false;

    [SerializeField]
    private CustomerDialog customerDialog;

    [SerializeField] private SpriteRenderer customer;
    private bool hasReachedTheCounter = false;
    public bool HasReachedTheCounter => hasReachedTheCounter;
    
    [SerializeField] private Image remainingTimeImage;

    // Counters
    private int customersServed = 0;
    private int moneyEarned = 0;

    [SerializeField] private int moneyPerDifficulty = 100;
    private int totalMoney = 0;
    public int TotalMoney => totalMoney;

    [SerializeField] private EndOfDayUI endOfDayUI;
    //[SerializeField] private TextMeshProUGUI goldGainedUI;
    [SerializeField] private Image goldCoinsImage;
    [SerializeField] UDictionary<Sprite, int> moneySprites;

    public float CurrentTimePercentage => currentDayTime / currentDay.dayTimeInSeconds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            Debug.Log($"Money: {totalMoney}");
            return;
        }

        moneyEarned = 0;
        customersServed = 0;
        //goldGainedUI.text = "Gold: " + moneyEarned.ToString();

        int index = 0;

        while (moneySprites.Values[index] < moneyEarned && index < moneySprites.Values.Count)
        {
            index++;
        }

        goldCoinsImage.sprite = moneySprites.Keys[index];
        if (index == 0)
        {
            goldCoinsImage.enabled = false;
        }
        else
        {
            goldCoinsImage.enabled = true;
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
        hasReachedTheCounter = false;
        /*
        if(currentDayNumberOfCustomers <= 0)
        {
            EndDay();
            return;
        }
        */
        int choice;
        do
        {
            choice = UnityEngine.Random.Range(0, currentDayOrders.Count);
        }
        while (currentDayOrders[choice] == currentOrder);

        currentOrder = currentDayOrders[choice];



        //Hacer que se muestre el texto en una burbuja
        orderDescription = currentOrder.description;
        //Debug.Log(currentOrder.description);

        //Me guardo el flavoyr type para que luego sea el que pides
        orderFlavourType = currentOrder.flavours;

        //Poner el sprite customer
        int count;
        do
        {
            count = UnityEngine.Random.Range(0, customerSprites.Count);
        }
        while (customerSprites[count] == currentCustomerSprite);

        currentCustomerSprite = customerSprites[count];
        currentNPCVoices = npcVoices[count];

        customer.sprite = currentCustomerSprite;
        currentDayNumberOfCustomers--;

        //customerDialog.SetBubbleMessage(orderDescription);
        //currentNPCVoices.OrderSounds.PlayAtPointRandom(transform.position);
        customer.color = new Color(customer.color.r, customer.color.g, customer.color.b, 0);

        Sequence mySequence = DOTween.Sequence();
        // Step 1: Fade from transparent to opaque
        mySequence.Append(customer.DOFade(1, 1f)); // Fade in over 1 second

        // Step 2: Move up and down in place for 1.5 seconds
        mySequence.Append(customer.transform.DOMoveY(customer.transform.position.y + 0.3f, 0.25f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutSine));

        // Step 3: Invoke a method at the end
        mySequence.OnComplete(ShowOrder);

        // Optional: Start the sequence
        mySequence.Play();

    }

    private void ShowOrder()
    {
        hasReachedTheCounter = true;
        customerDialog.SetBubbleMessage(orderDescription);
        currentNPCVoices.OrderSounds.PlayAtPointRandom(transform.position);
    }

    public bool ReciveBubba(UDictionary<FlavourType, int> bubbaFlavours)
    {
        bool correctOrder = false;

        correctOrder = CompareX(orderFlavourType, bubbaFlavours);
        //customerDialog.HideBubbleMessage();

        //esto habra que hacerlo despues, pero primero quiero unir las cosas
        Debug.Log(correctOrder);
        if (correctOrder)
        {
            customersServed++;
            moneyEarned += currentOrder.difficulty * moneyPerDifficulty;
            currentNPCVoices.CorrectOrderSounds.PlayAtPointRandom(transform.position);

            int index = 0;

            while (moneySprites.Values[index] < moneyEarned && index < moneySprites.Values.Count)
            {
                index++;
            }

            goldCoinsImage.sprite = moneySprites.Keys[index];
            if (index == 0)
            {
                goldCoinsImage.enabled = false;
            }
            else
            {
                goldCoinsImage.enabled = true;
            }

            //goldGainedUI.text = "Gold: " + moneyEarned.ToString();
        }
        else
        {
            currentNPCVoices.WrongOrderSounds.PlayAtPointRandom(transform.position);
        }
        hasReachedTheCounter = false;
        customerDialog.HideBubbleMessage();

        Sequence mySequence = DOTween.Sequence();
        // Step 1: Fade from transparent to opaque
        mySequence.Append(customer.transform.DOMoveY(customer.transform.position.y + 0.3f, 0.25f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutSine));

        // Step 2: Move up and down in place for 1.5 seconds
        mySequence.Append(customer.DOFade(0, 1f)); // Fade in over 1 second

        // Step 3: Invoke a method at the end
        mySequence.OnComplete(GenerateCustomer);

        // Optional: Start the sequence
        mySequence.Play();

        return correctOrder;
    }

    public void EndDay()
    {
        //codigo de final de dia 

        hasDayEnded = true;

        endOfDayUI.ShowUI(currentDay.day, customersServed, moneyEarned);
        totalMoney += moneyEarned;

        //Esto realmente se llamara cuando acabe la parte de final de dia pero por ahora la pongo aqui para probar
        Debug.Log("Acaba el dia");
        GenerateDay();

    }

    public void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentDayTime / 60F);
        int seconds = Mathf.FloorToInt(currentDayTime - minutes * 60);

        string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
        //remainingTimeText.text = timeText;
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
