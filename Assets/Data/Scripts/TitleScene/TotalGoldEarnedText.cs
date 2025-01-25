using TMPro;
using UnityEngine;

public class TotalGoldEarnedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        text.text = "You earned a total of " + LevelManager.Instance.TotalMoney.ToString() + " gold!";
    }
}
