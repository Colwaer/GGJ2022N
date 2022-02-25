using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        StartCoroutine(IFade(1.0f, 3.3f));
    }

    IEnumerator IFade(float waitTime, float time)
    {
        float waitTimer = 0f;
        while (waitTimer <waitTime)
        {
            waitTimer += Time.deltaTime;
            yield return null;
        }
        float originVolume = audioSource.volume;
        float timer = 0;
        audioSource.volume = 0;
        audioSource.Play();
        while (timer < time)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, originVolume, timer * timer / time * time);
            yield return null;
        }
    }
    private void OnDestroy()
    {
        audioSource.Stop();
    }
}
