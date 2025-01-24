using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterPartTooltip : MonoBehaviour
{
    [SerializeField] private MonsterPart monsterPart;
    public void SetMonsterPart(MonsterPart monsterPart) => this.monsterPart = monsterPart;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI bobaTeaPartText;
    [SerializeField] private List<TextMeshProUGUI> flavoursText;

    private void OnEnable()
    {
        nameText.text = monsterPart.Name;
        bobaTeaPartText.text = monsterPart.BobaTeaPart.ToString();
        for (int i = 0; i < monsterPart.Flavours.Count; i++)
        {
            flavoursText[i].gameObject.SetActive(true);
            flavoursText[i].text = monsterPart.Flavours.Keys[i].ToString() + ": " + monsterPart.Flavours.Values[i].ToString();
        }

        for (int i = monsterPart.Flavours.Count; i < flavoursText.Count; i++)
        {
            flavoursText[i].gameObject.SetActive(false);
            flavoursText[i].text = "";
        }
    }
}
