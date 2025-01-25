using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class BobaTeaCreationManager : MonoBehaviour
{
    public static BobaTeaCreationManager Instance { get; private set; }

    [SerializeField] private UDictionary<MonsterPart, int> monsterParts = new();
    //[SerializeField] private Transform monsterPartContainer;
    //private List<Image> monsterPartImages = new List<Image>();

    [SerializeField] private Transform monsterPartsContainer;
    private List<MonsterPartUIElement> toppingsMonsterParts = new();
    private List<MonsterPartUIElement> teaMonsterParts = new();
    private List<MonsterPartUIElement> ballsMonsterParts = new();

    [SerializeField] private UDictionary<FlavourType, Image> bobaTeaFlavoursBars;
    [SerializeField] private CanvasGroup sendBobaButton;
    [SerializeField] private CanvasGroup discardBobaButton;

    private UDictionary<FlavourType, int> bobaTeaStats = new UDictionary<FlavourType, int>();
    private Dictionary<BobaTeaPart, MonsterPart> bobaTeaPartsAddedDict = new Dictionary<BobaTeaPart, MonsterPart>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (Transform child in monsterPartsContainer.GetChild(0))
        {
            toppingsMonsterParts.Add(child.GetComponent<MonsterPartUIElement>());
            child.GetComponent<MonsterPartUIElement>().OnClickEvent.AddListener(AddMonsterPartToBoba);
        }

        foreach (Transform child in monsterPartsContainer.GetChild(1))
        {
            teaMonsterParts.Add(child.GetComponent<MonsterPartUIElement>());
            child.GetComponent<MonsterPartUIElement>().OnClickEvent.AddListener(AddMonsterPartToBoba);
        }

        foreach (Transform child in monsterPartsContainer.GetChild(2))
        {
            ballsMonsterParts.Add(child.GetComponent<MonsterPartUIElement>());
            child.GetComponent<MonsterPartUIElement>().OnClickEvent.AddListener(AddMonsterPartToBoba);
        }

        RefreshUI();
    }

    private void Start()
    {
        LevelManager.Instance.OnDayChanged.AddListener(SetUnlockedMonsterParts);
    }

    private void SetUnlockedMonsterParts(Day day)
    {
        int toppingsIndex = 0;
        int teaIndex = 0;
        int ballsIndex = 0;
        foreach (var pair in monsterParts.Where(x => x.Value <= day.day))
        {
            int chosenIndex = 0;
            switch (pair.Key.BobaTeaPart)
            {
                case BobaTeaPart.Toppings:
                    chosenIndex = toppingsIndex;
                    toppingsIndex++;
                    break;
                case BobaTeaPart.Tea:
                    chosenIndex = teaIndex;
                    teaIndex++;
                    break;
                case BobaTeaPart.Balls:
                    chosenIndex = ballsIndex;
                    ballsIndex++;
                    break;
            }
            UnlockMonsterPart(pair.Key, chosenIndex, pair.Key.BobaTeaPart);
        }

        foreach (var monsterPartImage in toppingsMonsterParts)
        {
            if (monsterPartImage.MonsterPart != null) continue;
            monsterPartImage.GetComponent<CanvasGroup>().alpha = 0f;
            monsterPartImage.GetComponent<CanvasGroup>().interactable = false;
            monsterPartImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        foreach (var monsterPartImage in teaMonsterParts)
        {
            if (monsterPartImage.MonsterPart != null) continue;
            monsterPartImage.GetComponent<CanvasGroup>().alpha = 0f;
            monsterPartImage.GetComponent<CanvasGroup>().interactable = false;
            monsterPartImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        foreach (var monsterPartImage in ballsMonsterParts)
        {
            if (monsterPartImage.MonsterPart != null) continue;
            monsterPartImage.GetComponent<CanvasGroup>().alpha = 0f;
            monsterPartImage.GetComponent<CanvasGroup>().interactable = false;
            monsterPartImage.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public List<MonsterPart> GetUnlockedMonsterParts(int day)
    {
        return monsterParts.Where(x => x.Value <= day).Select(x => x.Key).ToList();
    }

    private void UnlockMonsterPart(MonsterPart monsterPart, int i, BobaTeaPart bobaTeaPart)
    {
        List<MonsterPartUIElement> monsterPartImages = new();

        switch (bobaTeaPart)
        {
            case BobaTeaPart.Toppings:
                monsterPartImages = toppingsMonsterParts;
                break;
            case BobaTeaPart.Tea:
                monsterPartImages = teaMonsterParts;
                break;
            case BobaTeaPart.Balls:
                monsterPartImages = ballsMonsterParts;
                break;
        }

        if (i >= monsterPartImages.Count) Debug.Log("Out of bounds, not enough monster part images");

        monsterPartImages[i].GetComponent<CanvasGroup>().alpha = 1f;
        monsterPartImages[i].GetComponent<CanvasGroup>().interactable = true;
        monsterPartImages[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
        monsterPartImages[i].GetComponent<MonsterPartUIElement>().SetMonsterPart(monsterPart);
    }

    private void AddMonsterPartToBoba(MonsterPartUIElement monsterPartUIElement)
    {
        if (bobaTeaPartsAddedDict.ContainsKey(monsterPartUIElement.MonsterPart.BobaTeaPart))
        {
            Debug.Log("This part is already in the boba tea");
        }
        else
        {
            bobaTeaPartsAddedDict.Add(monsterPartUIElement.MonsterPart.BobaTeaPart, monsterPartUIElement.MonsterPart);

            foreach (var flavour in monsterPartUIElement.MonsterPart.Flavours.Keys)
            {
                if (!bobaTeaStats.ContainsKey(flavour))
                {
                    bobaTeaStats.Add(flavour, 0);
                }
                bobaTeaStats[flavour] = Mathf.Min (bobaTeaStats[flavour] + monsterPartUIElement.MonsterPart.Flavours[flavour], 3);
            }

            RefreshUI();
            Debug.Log($"Added {monsterPartUIElement.MonsterPart.Name} to the boba tea");
        }
    }

    public void DiscardBoba()
    {
        bobaTeaPartsAddedDict.Clear();
        bobaTeaStats.Clear();
        RefreshUI();
    }

    public void SendBoba()
    {
        LevelManager.Instance.ReciveBubba(bobaTeaStats);
        DiscardBoba();
    }

    private void RefreshUI()
    {
        // Refresh boba tea stats bars
        foreach (var flavour in bobaTeaFlavoursBars.Keys)
        {
            if (!bobaTeaStats.ContainsKey(flavour))
            {
                bobaTeaStats.Add(flavour, 0);
            }
            bobaTeaFlavoursBars[flavour].fillAmount = (float)bobaTeaStats[flavour] / 3f;
        }

        if (bobaTeaPartsAddedDict.Count > 0)
        {
            discardBobaButton.alpha = 1f;
            discardBobaButton.interactable = true;
            discardBobaButton.blocksRaycasts = true;
        }
        else
        {
            discardBobaButton.alpha = 0f;
            discardBobaButton.interactable = false;
            discardBobaButton.blocksRaycasts = false;
        }

        if (bobaTeaPartsAddedDict.Count == 3)
        {
            sendBobaButton.alpha = 1f;
            sendBobaButton.interactable = true;
            sendBobaButton.blocksRaycasts = true;
        }
        else
        {
            sendBobaButton.alpha = 0f;
            sendBobaButton.interactable = false;
            sendBobaButton.blocksRaycasts = false;
        }
    }
}
