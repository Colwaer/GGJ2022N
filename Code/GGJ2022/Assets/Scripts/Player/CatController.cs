using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CatController : MonoBehaviour
{
    public static CatController instance;

    private Cat whiteCat;
    private Cat blackCat;

    [SerializeField]
    private Cat currentCat;

    public bool EnablePressSwitch = true;
    public AudioClip transClip;
    public AudioClip remakeClip;
    AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        blackCat = GameObject.FindGameObjectWithTag("BlackCat").GetComponent<Cat>();
        whiteCat = GameObject.FindGameObjectWithTag("WhiteCat").GetComponent<Cat>();

        currentCat = blackCat;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        
        audioSource.volume = audioSource.volume / 3;
    }


    private void Update()
    {
        if (EnablePressSwitch && Input.GetKeyDown(KeyCode.Q) && currentCat.EnableSwitchCat())
            SwitchCat();
        //Debug.Log(currentCat + " is _Updating");
        currentCat._Update();
    }
    private void FixedUpdate()
    {
        currentCat._FixedUpdate();
    }
    public bool EnableSwitchCat()
    {
        return currentCat.EnableSwitchCat();
    }
    public bool CatOnSamePos()
    {
        Vector2 pos1 = blackCat.transform.position;
        Vector2 pos2 = whiteCat.transform.position;
        pos1.x = Mathf.Round(pos1.x);
        pos1.y = Mathf.Round(pos1.y);
        pos2.x = Mathf.Round(pos2.x);
        pos2.y = Mathf.Round(pos2.y);
        return pos1 == pos2;
    }
    public void SwitchCat()
    {
        // ≈–∂œ¡©√® «∑Ò÷ÿ∫œ
        if (CatOnSamePos())
            return;

        PlayTransSound();

        currentCat.Exit();
        currentCat.ToGrid();
        SpriteRenderer SR = currentCat.GetComponent<SpriteRenderer>();
        SR.sortingLayerName = "Cat Bottom";
        currentCat = currentCat == blackCat ? whiteCat : blackCat;
        SR = currentCat.GetComponent<SpriteRenderer>();
        SR.sortingLayerName = "Cat Top";
        if (currentCat.isGrid)
        {
            currentCat.ToCat();
        }
        currentCat.Enter();

    }

    public void Remake()
    {
        EnablePressSwitch = true;
        
        whiteCat.PlayDieAnim();
        blackCat.PlayDieAnim();
        audioSource.clip = remakeClip;
        audioSource.Play();
    }
    public void PlayTransSound()
    {
        Debug.Log("PlaytransSound");
        audioSource.clip = transClip;
        audioSource.Play();
    }
}
