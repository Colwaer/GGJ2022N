using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    bool needDestroyed = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level != 1 && !needDestroyed)
        {
            needDestroyed = true;
            StartCoroutine(IFade(0.5f));
        }
        else if (level != 1 && needDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator IFade(float time)
    {
        float originVolume = audioSource.volume;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(originVolume, 0, timer / time);
            yield return null;
        }
        Destroy(this.gameObject);
    }
    
}
