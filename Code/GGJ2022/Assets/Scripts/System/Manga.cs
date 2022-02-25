using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manga : MonoBehaviour
{
    Image manga;

    Color transparent;
    Color nonTransparent;
    float timer1 = 0f;
    float timer2 = 0f;
    float timer3 = 0f;
    bool first = false;
    bool second = false;
    private void Awake()
    {
        manga = GetComponent<Image>();
        transparent = manga.color;
        Color c = transparent;
        c.a = 1;
        nonTransparent = c;
    }

    private void Update()
    {
        if (timer1 < 3f)
        {
            timer1 += Time.deltaTime;
            manga.color = Color.Lerp(transparent, nonTransparent, timer1 / 3f);
        }
        else
        {
            first = true;
        }
        if (first && timer2 < 3f)
        {
            timer2 += Time.deltaTime;
        }
        else if (first)
        {
            second = true;
        }
        if (second && timer3 < 3f)
        {
            timer3 += Time.deltaTime;
            manga.color = Color.Lerp(nonTransparent, transparent, timer3 / 3f);
        }
        else if (second)
        {
            MySceneManager.Instance.LoadNextScene();
        }
    }


}
