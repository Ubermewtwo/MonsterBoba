using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BobaTeaCreationManager : MonoBehaviour
{
    [SerializeField] private UDictionary<MonsterPart, int> monsterParts;
    [SerializeField] private Transform monsterPartContainer;
    private List<Image> monsterPartImages = new List<Image>();
    [SerializeField] private UDictionary<FlavourType, Image> bobaTeaFlavoursBars;
    [SerializeField] private CanvasGroup sendBobaButton;
    [SerializeField] private CanvasGroup discardBobaButton;

    private UDictionary<FlavourType, int> bobaTeaStats = new UDictionary<FlavourType, int>();
    private Dictionary<BobaTeaPart, MonsterPart> bobaTeaPartsAddedDict = new Dictionary<BobaTeaPart, MonsterPart>();

    private void Awake()
    {
        foreach (Transform child in monsterPartContainer)
        {
            foreach (Transform grandChild in child)
            {
                monsterPartImages.Add(grandChild.GetComponent<Image>());
            }
        }

        RefreshUI();

        foreach (var image in monsterPartImages)
        {
            image.GetComponent<MonsterPartUIElement>().OnClickEvent.AddListener(AddMonsterPartToBoba);
        }
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
            monsterParts[monsterPartUIElement.MonsterPart] -= 1;

            if (monsterParts[monsterPartUIElement.MonsterPart] == 0)
            {
                monsterParts.Remove(monsterPartUIElement.MonsterPart);
            }

            foreach (var flavour in monsterPartUIElement.MonsterPart.Flavours.Keys)
            {
                if (!bobaTeaStats.ContainsKey(flavour))
                {
                    bobaTeaStats.Add(flavour, 0);
                }
                bobaTeaStats[flavour] = Mathf.Min(bobaTeaStats[flavour] + monsterPartUIElement.MonsterPart.Flavours[flavour], 5);
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

    public void AddMonsterPartToStorage(MonsterPart monsterPart)
    {
        if (monsterParts.ContainsKey(monsterPart))
        {
            monsterParts[monsterPart] += 1;
        }
        else
        {
            monsterParts.Add(monsterPart, 1);
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < monsterPartImages.Count && i < monsterParts.Keys.Count; i++)
        {
            monsterPartImages[i].GetComponent<CanvasGroup>().alpha = 1f;
            monsterPartImages[i].GetComponent<CanvasGroup>().interactable = true;
            monsterPartImages[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
            monsterPartImages[i].sprite = monsterParts.Keys[i].Sprite;
            monsterPartImages[i].GetComponentInChildren<TextMeshProUGUI>().text = monsterParts.Values[i].ToString();
            monsterPartImages[i].GetComponent<MonsterPartUIElement>().SetMonsterPart(monsterParts.Keys[i]);
        }

        for (int i = monsterParts.Count; i < monsterPartImages.Count; i++)
        {
            monsterPartImages[i].GetComponent<CanvasGroup>().alpha = 0f;
            monsterPartImages[i].GetComponent<CanvasGroup>().interactable = false;
            monsterPartImages[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        // Refresh boba tea stats bars
        foreach (var flavour in bobaTeaFlavoursBars.Keys)
        {
            if (!bobaTeaStats.ContainsKey(flavour))
            {
                bobaTeaStats.Add(flavour, 0);
            }
            bobaTeaFlavoursBars[flavour].fillAmount = (float)bobaTeaStats[flavour] / 5f;
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
