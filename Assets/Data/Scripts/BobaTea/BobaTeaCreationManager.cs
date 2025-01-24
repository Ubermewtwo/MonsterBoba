using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BobaTeaCreationManager : MonoBehaviour
{
    [SerializeField] private UDictionary<MonsterPart, int> monsterParts;
    [SerializeField] private List<Image> monsterPartImages = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < monsterPartImages.Count && i < monsterParts.Keys.Count; i++)
        {
            monsterPartImages[i].GetComponent<CanvasGroup>().alpha = 1f;
            monsterPartImages[i].GetComponent<CanvasGroup>().interactable = true;
            monsterPartImages[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
            monsterPartImages[i].sprite = monsterParts.Keys[i].Sprite;
            monsterPartImages[i].GetComponentInChildren<TextMeshProUGUI>().text = monsterParts.Values[i].ToString();
        }

        for (int i = monsterParts.Count; i < monsterPartImages.Count; i++)
        {
            monsterPartImages[i].GetComponent<CanvasGroup>().alpha = 0f;
            monsterPartImages[i].GetComponent<CanvasGroup>().interactable = false;
            monsterPartImages[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
