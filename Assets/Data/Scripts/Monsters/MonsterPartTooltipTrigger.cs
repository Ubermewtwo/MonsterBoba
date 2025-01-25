using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterPartUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private MonsterPart monsterPart;
    public MonsterPart MonsterPart => monsterPart;

    public UnityEvent<MonsterPartUIElement> OnClickEvent;

    public void SetMonsterPart(MonsterPart monsterPart)
    {
        this.monsterPart = monsterPart;
        GetComponent<Image>().sprite = monsterPart.Sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Instance.ShowMonsterPartTooltip(monsterPart, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.HideMonsterPartTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(this);
    }
}
