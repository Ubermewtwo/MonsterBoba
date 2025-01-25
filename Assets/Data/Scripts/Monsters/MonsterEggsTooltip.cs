using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterEggsTooltip : MonoBehaviour
{
    [SerializeField] private UDictionary<Monster, int> monsterEggs;
    public void SetMonsterEggs(UDictionary<Monster, int> monsterEggs) => this.monsterEggs = monsterEggs;

    [SerializeField] private GameObject monsterEggUIPrefab;

    private void OnEnable()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (var monsterEgg in monsterEggs.Keys)
        {
            var monsterEggUI = Instantiate(monsterEggUIPrefab, transform);
            monsterEggUI.GetComponentInChildren<Image>().sprite = monsterEgg.EggSprite;
            monsterEggUI.GetComponentInChildren<Image>().color = monsterEgg.EggColor;
            monsterEggUI.GetComponentInChildren<TextMeshProUGUI>().text = monsterEgg.Name + " Egg: " + monsterEggs[monsterEgg];
        }
    }
}
