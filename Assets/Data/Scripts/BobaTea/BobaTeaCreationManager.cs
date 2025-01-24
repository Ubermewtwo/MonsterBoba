using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BobaTeaCreationManager : MonoBehaviour
{
    [SerializeField] private UDictionary<MonsterPart, int> monsterParts;
    [SerializeField] private List<Image> monsterPartImages;
    [SerializeField] private UDictionary<FlavourType, Image> bobaTeaFlavoursBars;

    private UDictionary<FlavourType, int> bobaTeaStats = new UDictionary<FlavourType, int>();
    private Dictionary<BobaTeaPart, MonsterPart> bobaTeaStatsDict = new Dictionary<BobaTeaPart, MonsterPart>();

    private void Awake()
    {
        RefreshUI();

        foreach (var image in monsterPartImages)
        {
            image.GetComponent<MonsterPartUIElement>().OnClickEvent.AddListener(AddMonsterPartToBoba);
        }
    }

    private void AddMonsterPartToBoba(MonsterPartUIElement monsterPartUIElement)
    {
        if (bobaTeaStatsDict.ContainsKey(monsterPartUIElement.MonsterPart.BobaTeaPart))
        {
            Debug.Log("This part is already in the boba tea");
        }
        else
        {
            bobaTeaStatsDict.Add(monsterPartUIElement.MonsterPart.BobaTeaPart, monsterPartUIElement.MonsterPart);
            monsterParts[monsterPartUIElement.MonsterPart] -= 1;

            foreach (var flavour in monsterPartUIElement.MonsterPart.Flavours.Keys)
            {
                if (!bobaTeaStats.ContainsKey(flavour))
                {
                    bobaTeaStats.Add(flavour, 0);
                }
                bobaTeaStats[flavour] += monsterPartUIElement.MonsterPart.Flavours[flavour];
            }

            RefreshUI();
            Debug.Log($"Added {monsterPartUIElement.MonsterPart.Name} to the boba tea");
        }
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
    }
}
