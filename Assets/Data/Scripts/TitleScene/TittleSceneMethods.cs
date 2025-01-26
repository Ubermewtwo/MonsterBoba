using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleSceneMethods : MonoBehaviour
{
    [SerializeField] private AudioClipList hoverSounds;
    [SerializeField] private AudioClipList clickSounds;
    private bool isLoadingScene = false;

    public void PlayOnHoverSound()
    {
        hoverSounds.PlayAtPointRandom(transform.position);
    }

    public void PlayOnClickSound()
    {
        clickSounds.PlayAtPointRandom(transform.position);
    }

    public void LoadGameScene()
    {
        if (isLoadingScene) return;
        isLoadingScene = true;
        Invoke("LoadGameScenelater", 1f);
    }

    private void LoadGameScenelater()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTittleScene()
    {
        if (isLoadingScene) return;
        isLoadingScene = true;
        if (LevelManager.Instance != null)
        {
            Destroy(LevelManager.Instance.gameObject);
        }
        Invoke("LoadTittleScenelater", 1f);
    }

    private void LoadTittleScenelater()
    {
        SceneManager.LoadScene("TittleScene");
    }

    public void LoadTutorialScene()
    {
        if (isLoadingScene) return;
        isLoadingScene = true;
        Invoke("LoadTutorialScenelater", 1f);
    }

    private void LoadTutorialScenelater()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void ExitGamePlease()
    {
#if UNITY_EDITOR
        // Exit play mode in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // Quit the application
    Application.Quit();
#endif
    }

    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
