using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    CatController catController;

    private void Awake()
    {
        catController = GameObject.FindGameObjectWithTag("CatController").GetComponent<CatController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuPanel.activeInHierarchy)
                HideMenuPanel();
            else
                ShowMenuPanel();
        }
    }

    public void ShowMenuPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(true);
    }
    public void HideMenuPanel()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
    }
    public void BackToMainMenu()
    {
        MySceneManager.Instance.LoadScene(0);
    }
    public void BackToGame()
    {
        HideMenuPanel();
    }
    public void Remake()
    {
        catController.Remake();
        HideMenuPanel();
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
