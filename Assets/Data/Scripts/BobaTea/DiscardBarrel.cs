using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiscardBarrel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image barrelImage;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private CanvasGroup discardBarrelCanvasGroup;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (discardBarrelCanvasGroup.interactable == false) return;
        barrelImage.sprite = openSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        barrelImage.sprite = closedSprite;
    }
}
