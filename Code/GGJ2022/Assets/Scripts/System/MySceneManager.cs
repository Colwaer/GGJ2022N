using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    //private int Index => UnityEngine.SceneManagement.SceneManager.
    private int index = 0;
    private int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            PlayerPrefs.SetInt(index.ToString(), 1);
            if (index > 2)
                PlayerPrefs.SetInt("LastLevel", index);           
        }
    }
    private int sceneCount;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        sceneCount = SceneManager.sceneCountInBuildSettings;
        PlayerPrefs.SetInt("3", 1);
        PlayerPrefs.SetInt("2", 1);
    }



    public void LoadNextScene()
    {  
        index++;
        Debug.Log("LoadScene : " + index + "SceneCount : " + sceneCount);
        Index %= sceneCount;
        
        SceneManager.LoadScene(Index);
    }
    
    public void LoadLastLevel()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 2);
        Index = lastLevel;
        SceneManager.LoadScene(lastLevel);
    }


    public void LoadScene(int sceneIndex)
    {
        sceneIndex %= sceneCount;
        Index = sceneIndex;
        SceneManager.LoadScene(Index);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
