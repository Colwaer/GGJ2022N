using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Quit : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadNextLevel);
    }
    private void LoadNextLevel()
    {
        MySceneManager.Instance.Quit();
    }
}
