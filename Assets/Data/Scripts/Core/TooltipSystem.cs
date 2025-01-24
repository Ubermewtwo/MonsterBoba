using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance { get; private set; }

    [SerializeField] private GameObject monsterPartTooltipPrefab;

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
        monsterPartTooltipPrefab.transform.position = position;
        monsterPartTooltipPrefab.GetComponent<MonsterPartTooltip>().SetMonsterPart(monsterPart);
        monsterPartTooltipPrefab.gameObject.SetActive(true);
    }

    public void HideMonsterPartTooltip()
    {
        monsterPartTooltipPrefab.gameObject.SetActive(false);
    }
}
