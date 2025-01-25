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
        Invoke("LoadScene", 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGamePlease()
    {
        if (Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
