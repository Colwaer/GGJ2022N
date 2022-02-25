using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_ChooseLevel : MonoBehaviour
{
    Button btn;
    public int LevelIndex = 1;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.interactable = PlayerPrefs.GetInt(LevelIndex.ToString(), 0) == 1;
        btn.onClick.AddListener(LoadScene);
    }
    
    public void LoadScene()
    {
        MySceneManager.Instance.LoadScene(LevelIndex);
    }
}
