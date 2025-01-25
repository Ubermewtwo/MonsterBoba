using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance { get; private set; }

    [SerializeField] private GameObject monsterPartTooltip;
    [SerializeField] private GameObject monsterEggsTooltip;

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

    public void ShowMonsterPartTooltip(MonsterPart monsterPart, Vector3 position)
    {
        monsterPartTooltip.transform.position = position;
        monsterPartTooltip.GetComponent<MonsterPartTooltip>().SetMonsterPart(monsterPart);
        monsterPartTooltip.gameObject.SetActive(true);
    }

    public void HideMonsterPartTooltip()
    {
        monsterPartTooltip.gameObject.SetActive(false);
    }

    public void ShowMonsterEggsTooltip(UDictionary<Monster, int> monsterEggs, Vector3 position)
    {
        monsterEggsTooltip.transform.position = position;
        monsterEggsTooltip.GetComponent<MonsterEggsTooltip>().SetMonsterEggs(monsterEggs);
        monsterEggsTooltip.gameObject.SetActive(true);
    }

    public void HideMonsterEggsTooltip()
    {
        monsterEggsTooltip.gameObject.SetActive(false);
    }
}
