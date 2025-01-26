using UnityEngine;
using UnityEngine.Video;

public class ShowMainPanelAfterDelay : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoPlayer videoPlayerPuto;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        Invoke("ShowVideo", 7f);
        Invoke("ShowPanel", 11f);
    }

    private void ShowVideo()
    {
        videoPlayerPuto.gameObject.SetActive(false);
        videoPlayer.Play();
        music.Play();
    }

    private void ShowPanel()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
