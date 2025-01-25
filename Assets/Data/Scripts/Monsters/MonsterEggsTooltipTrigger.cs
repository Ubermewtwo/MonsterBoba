using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MonsterEggsTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UDictionary<Monster, int> monsterEggs = new UDictionary<Monster, int>(); // <monster egg, amount>

    public void OnPointerEnter(PointerEventData eventData)
    {
        // replace by gettin mosnter eggs from game manager
        TooltipSystem.Instance.ShowMonsterEggsTooltip(monsterEggs, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.HideMonsterEggsTooltip();
    }
}
