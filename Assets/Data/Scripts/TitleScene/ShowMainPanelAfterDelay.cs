using UnityEngine;

public class ShowMainPanelAfterDelay : MonoBehaviour
{
    private void Awake()
    {
        Invoke("ShowPanel", 4f);
    }

    private void ShowPanel()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
