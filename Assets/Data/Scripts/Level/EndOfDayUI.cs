using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndOfDayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayNumberText;
    [SerializeField] private TextMeshProUGUI customersServedText;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;
    [SerializeField] private Transform unlockedMonsterPartContainer;
    [SerializeField] private GameObject unlockedMonsterPartPrefab;
    [SerializeField] private CanvasGroup inputBlockingPanel;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowUI(int dayNumber, int customersServed, int moneyEarned)
    {
        dayNumberText.text = "Day " + dayNumber.ToString() + " Complete!";
        customersServedText.text = "Customers Served: " + customersServed.ToString();
        moneyEarnedText.text = "Money Earned: $" + moneyEarned.ToString();

        List<MonsterPart> unlockedMonsterParts = BobaTeaCreationManager.Instance.GetUnlockedMonsterParts(dayNumber + 1);

        for (int i = unlockedMonsterPartContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(unlockedMonsterPartContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < unlockedMonsterParts.Count; i++)
        {
            if (i >= unlockedMonsterPartContainer.childCount) Debug.Log("Out of bounds, not enough monster part images");

            Instantiate(unlockedMonsterPartPrefab, unlockedMonsterPartContainer).GetComponentInChildren<MonsterPartUIElement>().SetMonsterPart(unlockedMonsterParts[i]);
        }

        inputBlockingPanel.alpha = 1f;
        inputBlockingPanel.interactable = true;
        inputBlockingPanel.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public void HideUI()
    {
        inputBlockingPanel.alpha = 0f;
        inputBlockingPanel.interactable = false;
        inputBlockingPanel.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1f;
    }
}
