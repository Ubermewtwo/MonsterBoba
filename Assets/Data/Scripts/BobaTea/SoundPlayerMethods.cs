using UnityEngine;

public class SoundPlayerMethods : MonoBehaviour
{
    [SerializeField] private AudioClipList discardSounds;

    public void PlayDiscardSound()
    {
        discardSounds.PlayAtPointRandom(transform.position);
    }
}
