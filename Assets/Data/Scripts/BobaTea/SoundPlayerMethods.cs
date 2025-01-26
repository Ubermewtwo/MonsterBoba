using UnityEngine;

public class SoundPlayerMethods : MonoBehaviour
{
    [SerializeField] private AudioClipList discardSounds;
    [SerializeField] private AudioClipList hoverSounds;
    [SerializeField] private AudioClipList clickSounds;

    public void PlayDiscardSound()
    {
        discardSounds.PlayAtPointRandom(transform.position);
    }

    public void PlayHoverSound()
    {
        hoverSounds.PlayAtPointRandom(transform.position);
    }

    public void PlayClickSound()
    {
        clickSounds.PlayAtPointRandom(transform.position);
    }
}
