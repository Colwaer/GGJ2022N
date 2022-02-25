using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipMessage : MonoBehaviour
{
    public static TipMessage instance;

    public Image background;
    public Text word;
    public float displayTime;

    private WaitForSeconds waitSec;
    private float nowAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        word.text = "";
        waitSec = new WaitForSeconds(displayTime);
    }

    private void Update()
    {
        Color color = background.color;
        color.a = nowAlpha;
        background.color = color;
        color = word.color;
        color.a = nowAlpha;
        word.color = color;
    }

    private Tween lastTween;
    public void SetWord(string s)
    {
        word.text = s;
        StopAllCoroutines();
        lastTween?.Kill();
        nowAlpha = 1f;
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        yield return waitSec;
        lastTween = DOTween.To(() => nowAlpha, x => nowAlpha = x, 0, 0.5f).SetEase(Ease.OutSine);
    }
}
