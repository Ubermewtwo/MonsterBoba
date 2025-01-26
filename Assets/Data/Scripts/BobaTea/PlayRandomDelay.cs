using System.Collections;
using UnityEngine;

public class PlayRandomDelay : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(PlayStrawSound());
    }

    private IEnumerator PlayStrawSound()
    {
        while (true)
        {
            float delay = Random.Range(25, 35);
            yield return new WaitForSeconds(delay);
            GetComponent<AudioSource>().Play();
            yield return null;
        }
    }
}
